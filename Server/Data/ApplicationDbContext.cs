using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProgramPro.Shared.Models;
using System.Reflection.Metadata;

namespace ProgramPro.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Day> Days { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<BodyStatistics> BodyStatistics { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<ExerciseStatistics> ExerciseStatistics { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Pull Up", Description = "Pull the chin over the bar" },
                new Exercise { Id = 2, Name = "Back Squat", Description = "With a bar on the back, squat down to 90 degrees" },
                new Exercise { Id = 3, Name = "Bench Press", Description = "Press a bar from the chest" }
                );
        }

        public void SeedApplicationUsers(UserManager<ApplicationUser> userManager)
        {
            // Check if the user already exists
            if (userManager.FindByEmailAsync("user@test.dk").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user@test.dk",
                    Email = "user@test.dk",
                    EmailConfirmed = true
                    // Add other properties as needed
                };

                IdentityResult result = userManager.CreateAsync(user, "Test#123").Result;

                if (result.Succeeded)
                {
                    // Add roles to the user if needed
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public void AddRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }
        }

        public void ClearDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ClearDatabase();
            AddRoles(roleManager);
            SeedApplicationUsers(userManager);
        }
    }
}