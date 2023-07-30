using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProgramPro.Shared.Models;
using System.Reflection.Metadata;

namespace ProgramPro.Server.Data
{
    public class ProgramProDbContext : DbContext
    {
        public ProgramProDbContext(DbContextOptions<ProgramProDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Day> Days { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Trainingprogram> Trainingprograms { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<BodyStatistics> BodyStatistics { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<ExerciseStatistics> ExerciseStatistics { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<Set> Set { get; set; }
    }
}