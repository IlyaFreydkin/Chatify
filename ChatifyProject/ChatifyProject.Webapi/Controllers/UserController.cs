using AutoMapper;
using ChatifyProject.Application.Dto;
using ChatifyProject.Application.Infrastructure;
using ChatifyProject.Application.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UserController : ControllerBase
{
    // DTO class for the JSON body of the login request
    public record CredentialsDto(string username, string password);

    private readonly ChatifyContext _db;
    private readonly IConfiguration _config;
    private readonly bool _isDevelopment;

    public UserController(IHostEnvironment _env, IConfiguration config, ChatifyContext db)
    {
        _config = config;
        _isDevelopment = _env.IsDevelopment();
        _db = db;
    }
    
    /// <summary>
    /// POST /api/user/login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CredentialsDto credentials)
    {
        var lifetime = TimeSpan.FromHours(3);
        var searchuser = _config["Searchuser"];
        var searchpass = _config["Searchpass"];
        var secret = Convert.FromBase64String(_config["Secret"]);
        var localAdmins = _config["LocalAdmins"].Split(",");

        using var service = _isDevelopment && !string.IsNullOrEmpty(searchuser)
            ? AdService.Login(searchuser, searchpass, credentials.username)
            : AdService.Login(credentials.username, credentials.password);

        var currentUser = service.CurrentUser;
        if (currentUser is null) { return Unauthorized(); }
        //register in login
        var user = await _db.Users.FirstOrDefaultAsync(a => a.Name == credentials.username);
        if (user is null) 
        {
            user = new User(credentials.username, credentials.password, "guest@gmail.com", Userrole.User);
            await _db.Users.AddAsync(user);
            try { await _db.SaveChangesAsync(); }
            catch (DbUpdateException) { return BadRequest(); }
        }
        else if (!user.CheckPassword(credentials.password)) { return Unauthorized(); }

        var role = localAdmins.Contains(currentUser.Cn)
                        ? AdUserRole.Management.ToString() : currentUser.Role.ToString();
        var group = (currentUser.Role, currentUser.Classes.Length > 0) switch
        {
            (AdUserRole.Pupil, true) => currentUser.Classes[0],
            (AdUserRole.Pupil, false) => "Unknown class",
            (AdUserRole.Teacher, _) => AdUserRole.Teacher.ToString(),
            (AdUserRole.Management, _) => AdUserRole.Teacher.ToString(),
            (_, _) => AdUserRole.Administration.ToString()
        };
        if (currentUser.Classes.Length > 0)
        {
            user.Group = currentUser.Classes[0];
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Payload for our JWT.
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentUser.Cn),
                new Claim(ClaimTypes.Name, currentUser.Cn),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim("Group", group)
            }),
            Expires = DateTime.UtcNow + lifetime,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new
        {
            Username = currentUser.Cn,
            Role = role,
            Group = group,
            Token = tokenHandler.WriteToken(token)
        });
    }

    /// <summary>
    /// GET /api/user/me
    /// Gets information about the current (authenticated) user.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetUserdata()
    {
        var user = HttpContext.User.Identity?.Name;
        return Ok(new
        {
            Username = HttpContext.User.Identity?.Name,
            Group = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Group")?.Value
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _db.Users
            .Select(u => new
            {
                u.Name,
                u.Password,
                u.Email,
                u.Role,
                u.Group
            })
            .ToListAsync();
        if (user is null) { return BadRequest(); }
        return Ok(user);
    }

    /// <summary>
    /// POST /api/user/register
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CredentialsDto credentials)
    {
        var user = await _db.Users.FirstOrDefaultAsync(a => a.Name == credentials.username);
        if (user != null) { return BadRequest("User already exists"); }
        user = new User(credentials.username, credentials.password, "guest@gmail.com", Userrole.User);
        await _db.Users.AddAsync(user);
        try { await _db.SaveChangesAsync(); }
        catch (DbUpdateException) { return BadRequest("Could not register user"); }
        return Ok();
    }

    

}
