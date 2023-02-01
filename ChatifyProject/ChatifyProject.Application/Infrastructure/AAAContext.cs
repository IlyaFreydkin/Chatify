using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace TripleAProject.Webapi.Infrastructure
{
    public class AAAContext : DbContext
    {
        public AAAContext(DbContextOptions opt) : base(opt) { }

        public DbSet<User> Users => Set<User>();

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

        public void Seed()
        {

            Randomizer.Seed = new Random(1619);
            var faker = new Faker("de");

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                return new User(
                    name: f.Name.LastName().ToLower(),
                    email: $"{f.Name.FirstName()}@gmail.at",
                    password: "1111",
                    role: f.PickRandom<Userrole>())

                { Guid = f.Random.Guid() };
            })
            .Generate(20)
            .ToList();
            Users.AddRange(users);
            SaveChanges();
        }
    }
}

