using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProgramPro.Server.Controllers;
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
        public DbSet<ComponentDefinition> ComponentDefinitions { get; set; }
        public DbSet<DayDefinition> DayDefinitions { get; set; }
        public DbSet<WorkoutExerciseDefinition> WorkoutExerciseDefinitions { get; set; }
        public DbSet<SetDefinition> SetDefinitions { get; set; }
        public DbSet<Component> Components { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.TrainingPrograms)
                .WithOne(trainingProgram => trainingProgram.ApplicationUser)
                .HasForeignKey(trainingProgram => trainingProgram.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.ComponentDefinitions)
                .WithOne(splitDefinition => splitDefinition.ApplicationUser)
                .HasForeignKey(splitDefinition => splitDefinition.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingProgram>()
                .HasMany(p => p.Goals)
                .WithOne(c => c.TrainingProgram)
                .HasForeignKey(x => x.TrainingProgramId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingProgram>()
                .HasMany(p => p.Components)
                .WithOne(c => c.TrainingProgram)
                .HasForeignKey(x => x.TrainingProgramId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Component>()
                .HasMany(p => p.Days)
                .WithOne(c => c.Component)
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Day>()
                .HasMany(p => p.WorkoutExercises)
                .WithOne(c => c.Day)
                .HasForeignKey(x => x.DayId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExerciseStatistics>()
                .HasMany(p => p.PersonalRecords)
                .WithOne(c => c.ExerciseStatistics)
                .HasForeignKey(x => x.ExerciseStatisticsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Statistics>()
                .HasMany(p => p.ExerciseStatistics)
                .WithOne(c => c.Statistics)
                .HasForeignKey(x => x.StatisticsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Statistics>()
                .HasMany(p => p.BodyStatistics)
                .WithOne(c => c.Statistics)
                .HasForeignKey(x => x.StatisticsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasMany(p => p.Sets)
                .WithOne(c => c.WorkoutExercise)
                .HasForeignKey(x => x.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Set>()
                .HasOne(p => p.Entry)
                .WithOne(c => c.Set)
                .HasForeignKey<Entry>(x => x.SetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComponentDefinition>()
                .HasMany(p => p.DayDefinitions)
                .WithOne(c => c.ComponentDefinition)
                .HasForeignKey(x => x.ComponentDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DayDefinition>()
                .HasMany(p => p.WorkoutExerciseDefinitions)
                .WithOne(c => c.DayDefinition)
                .HasForeignKey(x => x.DayDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExerciseDefinition>()
                .HasMany(p => p.SetDefinitions)
                .WithOne(c => c.WorkoutExerciseDefinition)
                .HasForeignKey(x => x.WorkoutExerciseDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Pull Up", Description = "Pull the chin over the bar" },
                new Exercise { Id = 3, Name = "Back Squat", Description = "With a bar on the back, squat down to 90 degrees" },
                new Exercise { Id = 2, Name = "Bench Press", Description = "Press a bar from the chest" }
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

        public void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            AddRoles(roleManager);
            SeedApplicationUsers(userManager);
        }
    }
}