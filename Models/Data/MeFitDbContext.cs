using MeFit.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MeFit.Models.Data
{
    public class MeFitDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MFProgram> Programs { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public MeFitDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 1,
                Name = "Barbell curl",
                Description = "Biceps strong",
                ImageURL = "img",
                VideoURL = ".mov",
                TargetMuscleGroup = "biceps"
            });

            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 2,
                Name = "Leg press",
                Description = "Legs strong",
                ImageURL = "img",
                VideoURL = ".mov",
                TargetMuscleGroup = "legs"
            });

            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 3,
                Name = "Push up",
                Description = "Chest strong",
                ImageURL = "img",
                VideoURL = ".mov",
                TargetMuscleGroup = "upper body"
            });

            modelBuilder.Entity<Exercise>().HasData(new Exercise
            {
                Id = 4,
                Name = "Isolation curl",
                Description = "Biceps strong",
                ImageURL = "img",
                VideoURL = ".mov",
                TargetMuscleGroup = "biceps"
            });

            modelBuilder.Entity<Set>().HasData(new Set
            {
                Id = 1,
                ExerciseRepetitions = 10,
                ExerciseId = 1
            });

            modelBuilder.Entity<Set>().HasData(new Set
            {
                Id = 2,
                ExerciseRepetitions = 20,
                ExerciseId = 2
            });

            modelBuilder.Entity<Workout>().HasData(new Workout
            {
                Id = 1,
                Name = "Arm day",
                Type = "Strength",
                Complete = false
            });

            modelBuilder.Entity<Workout>().HasData(new Workout
            {
                Id = 2,
                Name = "Leg day",
                Type = "Strength",
                Complete = true
            });

            modelBuilder.Entity<Workout>().HasData(new Workout
            {
                Id = 3,
                Name = "Running",
                Type = "Cardio",
                Complete = false
            });

            modelBuilder.Entity<MFProgram>().HasData(new MFProgram
            {
                Id = 1,
                Name = "Bicep enhancement",
                Category = "Upper body"
            });

            modelBuilder.Entity<MFProgram>().HasData(new MFProgram
            {
                Id = 2,
                Name = "Strength building",
                Category = "Whole body"
            });

            modelBuilder.Entity<MFProgram>().HasData(new MFProgram
            {
                Id = 3,
                Name = "Cardio",
                Category = "Whole body"
            });

            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 1,
                ProfileId = "keycloak-uid",
                ProgramEndDate = DateTime.Now,
                Achieved = false,
                ProgramId = 1
            });

            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 2,
                ProfileId = "keycloak-uid",
                ProgramEndDate = DateTime.Now,
                Achieved = true,
                ProgramId = 2
            });

            modelBuilder.Entity<Goal>().HasData(new Goal
            {
                Id = 3,
                ProfileId = "keycloak-uid",
                ProgramEndDate = DateTime.Now,
                Achieved = true,
                ProgramId = 2
            });

            modelBuilder.Entity<Profile>().HasData(new Profile
            {
                Id = "keycloak-uid",
                AddressId = 1,
                ProgramId = 1,
                WorkoutId = 1,
                SetId = 1,
                Weight = 60,
                Height = 171,
                MedicalConditions = "Anxiety",
                Disabilities = "none"
            });

            modelBuilder.Entity<Address>().HasData(new Address
            {
                Id = 1,
                AddressLine1 = "Hans Nielsen Hauges Gate 10",
                AddressLine2 = null,
                AddressLine3 = null,
                City =  "Trondheim",
                PostalCode = "7067",
                Contry = "Norway"
            });

            modelBuilder.Entity<Address>().HasData(new Address
            {
                Id = 2,
                AddressLine1 = "Høgreina 18c",
                AddressLine2 = null,
                AddressLine3 = null,
                City = "Trondheim",
                PostalCode = "7079",
                Contry = "Norway"
            });

            modelBuilder.Entity<Workout>()
                .HasMany(s => s.Sets)
                .WithMany(w => w.Workouts)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkoutSets",
                    r => r.HasOne<Set>().WithMany().HasForeignKey("SetId"),
                    l => l.HasOne<Workout>().WithMany().HasForeignKey("WorkoutId"),
                    je =>
                    {
                        je.HasKey("SetId", "WorkoutId");
                        je.HasData(
                            new { WorkoutId = 1, SetId = 1 },
                            new { WorkoutId = 1, SetId = 2 },
                            new { WorkoutId = 2, SetId = 1 }
                        );
                    });

            modelBuilder.Entity<Workout>()
                .HasMany(g => g.Goals)
                .WithMany(w => w.Workouts)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkoutGoals",
                    r => r.HasOne<Goal>().WithMany().HasForeignKey("GoalId"),
                    l => l.HasOne<Workout>().WithMany().HasForeignKey("WorkoutId"),
                    je =>
                    {
                        je.HasKey("GoalId", "WorkoutId");
                        je.HasData(
                            new { WorkoutId = 1, GoalId = 1 },
                            new { WorkoutId = 2, GoalId = 2 }
                        );
                    });

            modelBuilder.Entity<Workout>()
                .HasMany(p => p.Programs)
                .WithMany(w => w.Workouts)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkoutPrograms",
                    r => r.HasOne<MFProgram>().WithMany().HasForeignKey("ProgramId"),
                    l => l.HasOne<Workout>().WithMany().HasForeignKey("WorkoutId"),
                    je =>
                    {
                        je.HasKey("ProgramId", "WorkoutId");
                        je.HasData(
                            new { WorkoutId = 1, ProgramId = 1 },
                            new { WorkoutId = 1, ProgramId = 2 },
                            new { WorkoutId = 2, ProgramId = 2 },
                            new { WorkoutId = 3, ProgramId = 3 }
                        );
                    });
        }
    }
}
