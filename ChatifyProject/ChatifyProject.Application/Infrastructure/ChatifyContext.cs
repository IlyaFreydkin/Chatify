using AutoMapper;
using Bogus;
using ChatifyProject.Application.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChatifyProject.Application.Infrastructure
{
    public class ChatifyContext : DbContext
    {
        public ChatifyContext(DbContextOptions opt) : base(opt) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Userprofile> Profiles => Set<Userprofile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }
        }

        /// <summary>
        /// Initialize the database with some values (holidays, ...).
        /// Unlike Seed, this method is also called in production.
        /// </summary>
        private void Initialize()
        {
            var ilya = new User(
                name: "Ilya",
                password: "1111",
                email: "fre22343@spengergasse.at",
                role: Userrole.Admin
            );
            new Userprofile(
                firstname: "Ilya", 
                lastname: "Freydkin", 
                user: ilya, 
                description: "Ich mag Pizza"
            );

            var ahmed = new User(
                name: "Ahmed",
                password: "1111",
                email: "ahm22106@spengergasse.at",
                role: Userrole.User
            );
            new Userprofile(
                firstname: "Ahmed",
                lastname: "Khalid",
                user: ahmed,
                description: "Ich mag Dönner"
            );

            //var richard = new User(
            //    name: "Richard",
            //    password: "1111",
            //    email: "liu22291@spengergasse.at",
            //    role: Userrole.Admin
            //);
            //new Userprofile(
            //    firstname: "Richard",
            //    lastname: "Liu",
            //    user: richard,
            //    description: "Ich mag Reis"
            //);

            Users.Add(ilya);
            Users.Add(ahmed);
            //Users.Add(richard);

            SaveChanges();
        }

        /// <summary>
        /// Generates random values for testing the application. This method is only called in development mode.
        /// </summary>
        /// 
        private void Seed()
        {
            Randomizer.Seed = new Random(1619);
            var faker = new Faker("de");

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var user = new User(
                    name: f.Name.LastName().ToLower(),
                    email: $"{f.Name.FirstName()}@gmail.at",
                    password: "1111",
                    role: f.PickRandom<Userrole>())
                { Guid = f.Random.Guid() };

                var profile = new Userprofile(
                    firstname: f.Name.FirstName().ToLower(),
                    lastname: f.Name.LastName().ToLower(),
                    user: user,
                    description: "Das ist ein Beschreibung!")
                { Guid = f.Random.Guid() };

                user.Profile = profile;

                return user;
            })
            .Generate(20)
            .ToList();
            Users.AddRange(users);
            SaveChanges();
        }

        /// <summary>
        /// Creates the database. Called once at application startup.
        /// </summary>
        public void CreateDatabase(bool isDevelopment)
        {
            if (isDevelopment) { Database.EnsureDeleted(); }
            // EnsureCreated only creates the model if the database does not exist or it has no
            // tables. Returns true if the schema was created.  Returns false if there are
            // existing tables in the database. This avoids initializing multiple times.
            if (Database.EnsureCreated()) { Initialize(); }
            if (isDevelopment) Seed();
        }
    }
}


