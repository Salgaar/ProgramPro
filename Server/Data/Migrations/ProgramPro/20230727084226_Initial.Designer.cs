﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProgramPro.Server.Data;

#nullable disable

namespace ProgramPro.Server.Data.Migrations.ProgramPro
{
    [DbContext(typeof(ProgramProDbContext))]
    [Migration("20230727084226_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProgramPro.Shared.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.BodyStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatisticsId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("StatisticsId");

                    b.ToTable("BodyStatistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("WeekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeekId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PercentageOfOneRepMax")
                        .HasColumnType("int");

                    b.Property<int>("RIR")
                        .HasColumnType("int");

                    b.Property<double>("RPE")
                        .HasColumnType("float");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("SetId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SetId")
                        .IsUnique();

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.ExerciseStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("StatisticsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("StatisticsId");

                    b.ToTable("ExerciseStatistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.PersonalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExerciseStatisticsId")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseStatisticsId");

                    b.ToTable("PersonalRecords");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PercentageOfOneRepMax")
                        .HasColumnType("int");

                    b.Property<int>("RIR")
                        .HasColumnType("int");

                    b.Property<double>("RPE")
                        .HasColumnType("float");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Statistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Trainingprogram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trainingprograms");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Week", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.BodyStatistics", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Statistics", "Statistics")
                        .WithMany("BodyStatistics")
                        .HasForeignKey("StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Day", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Week", "Week")
                        .WithMany("Days")
                        .HasForeignKey("WeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Week");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Entry", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Set", "Set")
                        .WithOne("Entry")
                        .HasForeignKey("ProgramPro.Shared.Models.Entry", "SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Set");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.ExerciseStatistics", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProgramPro.Shared.Models.Statistics", "Statistics")
                        .WithMany("ExerciseStatistics")
                        .HasForeignKey("StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Goal", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Trainingprogram", "Program")
                        .WithOne("Goal")
                        .HasForeignKey("ProgramPro.Shared.Models.Goal", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.PersonalRecord", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.ExerciseStatistics", "ExerciseStatistics")
                        .WithMany("PersonalRecords")
                        .HasForeignKey("ExerciseStatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseStatistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Result", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Trainingprogram", "Program")
                        .WithOne("Result")
                        .HasForeignKey("ProgramPro.Shared.Models.Result", "ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Set", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Workout", "Workout")
                        .WithMany("Set")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Statistics", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.ApplicationUser", "User")
                        .WithMany("Statistics")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Trainingprogram", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.ApplicationUser", "User")
                        .WithMany("Programs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Week", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Trainingprogram", "Program")
                        .WithMany("Weeks")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Workout", b =>
                {
                    b.HasOne("ProgramPro.Shared.Models.Day", "Day")
                        .WithMany("Workouts")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProgramPro.Shared.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.ApplicationUser", b =>
                {
                    b.Navigation("Programs");

                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Day", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.ExerciseStatistics", b =>
                {
                    b.Navigation("PersonalRecords");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Set", b =>
                {
                    b.Navigation("Entry");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Statistics", b =>
                {
                    b.Navigation("BodyStatistics");

                    b.Navigation("ExerciseStatistics");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Trainingprogram", b =>
                {
                    b.Navigation("Goal");

                    b.Navigation("Result");

                    b.Navigation("Weeks");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Week", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("ProgramPro.Shared.Models.Workout", b =>
                {
                    b.Navigation("Set");
                });
#pragma warning restore 612, 618
        }
    }
}
