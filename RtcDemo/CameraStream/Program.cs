using CameraStream.Hubs;
using CameraStream.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddSignalR();
builder.Services.AddSingleton<List<User>>();
builder.Services.AddSingleton<List<Connection>>();
builder.Services.AddSingleton<List<Call>>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthorization();
//app.UseStreamSocket();
app.MapRazorPages();
app.MapHub<ConnectionHub>("/cnnctn", options =>
{
    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
});
app.Run();
