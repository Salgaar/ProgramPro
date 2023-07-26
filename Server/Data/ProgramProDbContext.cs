using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProgramPro.Server.Models;

namespace ProgramPro.Server.Data
{
    public class ProgramProDbContext : DbContext
    {
        public ProgramProDbContext(DbContextOptions<ProgramProDbContext> options) : base(options)
        {
        }

        public DbSet<CompletedWorkout> CompletedWorkouts { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}