using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgramPro.Shared.Models;
using Microsoft.Data.SqlClient;
using Humanizer;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.CustomTypeProviders;

namespace ProgramPro.Server.Data
{
    public static class TrainingProgramSeeder
    {
        public static void SeedData(ApplicationDbContext _context, UserManager<ApplicationUser> userManager)
        {
            var user = userManager.FindByNameAsync("user@test.dk").Result;
            // Opret et træningsprogram
            var startDate = new DateTime(2020, 1, 6);
            var programId = Guid.NewGuid();
            _context.Add(new TrainingProgram
            {
                Id = programId,
                Name = "Dit træningsprogram",
                Description = "Beskrivelse af dit træningsprogram",
                ApplicationUserId = user.Id, // Replace with the relevant user id
                StartDate = startDate,
                Active = false
            });

            // Opret 6 komponenter med 10 dage hver
            for (int i = 1; i <= 6; i++)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Components] ON");

                    _context.Add(new Component
                    {
                        Id = i,
                        Name = $"Component {i}",
                        Description = $"Beskrivelse af komponent {i}",
                        TrainingProgramId = programId,
                        ComponentNumber = i
                    });

                    _context.SaveChanges();

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Components] OFF");

                    transaction.Commit();
                }

                for (int j = 1; j <= 10; j++)
                {
                    var currentDate = startDate.AddDays((i - 1) * 10 + (j - 1));
                    var day = new Day
                    {
                        Id = (i - 1) * 10 + j,
                        ComponentId = i,
                        Date = currentDate,
                        Type = GetDayType(currentDate)
                    };
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Days] ON");

                        _context.Add(day);

                        _context.SaveChanges();

                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Days] OFF");

                        transaction.Commit();
                    }

                    if(day.Type == DayType.Training)
                    {
                        if(IsUpper(day.Date))
                        {
                            for (int k = 1; k <= 2; k++)
                            {
                                var exerciseId = k; // Vælg en øvelse - 1, 2 eller 3
                                var workoutExercise = new WorkoutExercise
                                {
                                    Id = ((i - 1) * 10 + j - 1) * 3 + k,
                                    DayId = day.Id,
                                    ExerciseId = exerciseId
                                };
                                using (var transaction = _context.Database.BeginTransaction())
                                {
                                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkoutExercises] ON");

                                    _context.Add(workoutExercise);

                                    _context.SaveChanges();

                                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkoutExercises] OFF");

                                    transaction.Commit();
                                }

                                // Opret planlagte sæt
                                var plannedSets = GeneratePlannedSets(exerciseId, startDate, currentDate, workoutExercise.Id);
                                foreach (var plannedSet in plannedSets)
                                {
                                    _context.Add(plannedSet);
                                }
                                _context.SaveChanges();

                                // Opret faktiske resultater for sæt
                                var actualSets = GenerateActualSets(plannedSets, i); // Pass the component number

                                for(int o = 0; o < 3; o++)
                                {
                                    var random = new Random();
                                    int l = random.Next(0, 30);
                                    if (actualSets.Count > l)
                                    {
                                        actualSets.Remove(actualSets[l]);
                                    }
                                }

                                foreach (var actualSet in actualSets)
                                {
                                    _context.Add(actualSet);
                                }
                                _context.SaveChanges();

                            }
                        }
                        else
                        {
                            var exerciseId = 3; // Vælg en øvelse - 1, 2 eller 3
                            var workoutExercise = new WorkoutExercise
                            {
                                Id = ((i - 1) * 10 + j - 1) * 3 + exerciseId,
                                DayId = day.Id,
                                ExerciseId = exerciseId
                            };
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkoutExercises] ON");

                                _context.Add(workoutExercise);

                                _context.SaveChanges();

                                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[WorkoutExercises] OFF");

                                transaction.Commit();
                            }

                            // Opret planlagte sæt
                            var plannedSets = GeneratePlannedSets(exerciseId, startDate, currentDate, workoutExercise.Id);
                            foreach (var plannedSet in plannedSets)
                            {
                                _context.Add(plannedSet);
                            }
                            _context.SaveChanges();


                            // Opret faktiske resultater for sæt
                            var actualSets = GenerateActualSets(plannedSets, i); // Pass the component number

                            for (int o = 0; o < 3; o++)
                            {
                                var random = new Random();
                                int l = random.Next(0, 30);
                                if (actualSets.Count > l)
                                {
                                    actualSets.Remove(actualSets[l]);
                                }
                            }

                            foreach (var actualSet in actualSets)
                            {
                                _context.Add(actualSet);
                            }
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        private static DayType GetDayType(DateTime date)
        {
            DayType dayType;
            switch(date.DayOfWeek)
            {
                case DayOfWeek.Wednesday:
                    dayType = DayType.Rest;
                    break;
                case DayOfWeek.Thursday:
                    dayType = DayType.Rest;
                    break;
                case DayOfWeek.Sunday:
                    dayType = DayType.Rest;
                    break;
                default:
                    dayType = DayType.Training;
                    break;
            }
            return dayType;
        }

        private static bool IsUpper(DateTime date)
        {
            bool isUpper;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    isUpper = true;
                    break;
                case DayOfWeek.Saturday:
                    isUpper = true;
                    break;
                default:
                    isUpper = false;
                    break;
            }
            return isUpper;
        }

        private static List<Set> GeneratePlannedSets(int exerciseId, DateTime startDate, DateTime currentDate, int workoutExerciseId)
        {
            // Implementer logikken til at generere planlagte sæt med gradvis ændring i RIR
            var daysPassed = (currentDate - startDate).TotalDays;
            var percentDecrease = daysPassed * 0.10;

            var plannedSets = new List<Set>();
            for (int i = 1; i <= 3; i++)
            {
                var set = new Set
                {
                    Weight = CalculateWeight(exerciseId, startDate, currentDate),
                    Reps = 5,
                    UsingRIR = true,
                    RIR = CalculateRIR(5, percentDecrease),
                    Type = SetType.Heavy,
                    WorkoutExerciseId = workoutExerciseId
                };
                plannedSets.Add(set);
            }

            return plannedSets;
        }

        private static List<Entry> GenerateActualSets(List<Set> plannedSets, int componentNumber)
        {
            // Implement the logic to generate actual sets with variations in reps and RIR
            var actualSets = new List<Entry>();
            var random = new Random();

            foreach (var plannedSet in plannedSets)
            {

                    var actualReps = plannedSet.Reps - random.Next(0, 2); // Variation in reps
                    var plannedRIRRange = plannedSet.RIR.Split('-');
                    var minRIR = int.Parse(plannedRIRRange[0]);
                    var maxRIR = int.Parse(plannedRIRRange[1]);
                    var actualRIR = random.Next(minRIR, maxRIR + 1); // Variation in RIR

                    actualSets.Add(new Entry
                    {
                        SetId = plannedSet.Id,
                        Weight = plannedSet.Weight,
                        Reps = actualReps,
                        RIR = actualRIR.ToString(),
                    });
                
                
            }

            return actualSets;
        }

        private static double CalculateWeight(int exerciseId, DateTime startDate, DateTime currentDate)
        {
            // Define the initial 1RM values for your exercises
            var initial1RMs = new Dictionary<int, double>
            {
                { 1, 50 },   // Initial 1RM for Pull Up
                { 2, 170 },  // Initial 1RM for Back Squat
                { 3, 135 }   // Initial 1RM for Bench Press
            };

            // Get the initial 1RM for the exercise
            var initial1RM = initial1RMs[exerciseId];

            // Calculate the number of days elapsed
            var totalDays = (currentDate - startDate).TotalDays;

            // Define the weight increment (1.25kg)
            var weightIncrement = 1.25;

            // Calculate the weight increase as 5% over 60 days
            var weightIncrease = initial1RM * 0.10 * totalDays / 60.0;

            // Ensure that the weight increase is in 1.25kg increments
            var roundedIncrease = Math.Ceiling(weightIncrease / weightIncrement) * weightIncrement;

            // Calculate the new weight
            var weight = initial1RM + roundedIncrease;

            return weight;
        }

        private static string CalculateRIR(int reps, double percentDecrease)
        {
            // Implementer logikken til at beregne RIR baseret på reps og procentvis stigning
            // Erstat denne logik med din egen beregning af RIR
            var minRIR = Math.Max(0, 2 + (int)(percentDecrease * -0.5));
            var maxRIR = Math.Max(0, 3 + (int)(percentDecrease * -0.5));
            return $"{minRIR}-{maxRIR}";
        }
    }
}
