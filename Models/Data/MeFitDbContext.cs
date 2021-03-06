using MeFit.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MeFit.Models.Data
{
    public class MeFitDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MFProgram> Programs { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<GoalWorkouts> GoalWorkouts { get; set; }

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
                Type = "Strength"
            });

            modelBuilder.Entity<Workout>().HasData(new Workout
            {
                Id = 2,
                Name = "Leg day",
                Type = "Strength"
            });

            modelBuilder.Entity<Workout>().HasData(new Workout
            {
                Id = 3,
                Name = "Running",
                Type = "Cardio"
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
                Weight = 60,
                Height = 171,
                MedicalConditions = "Anxiety",
                Disabilities = "none"
            });


            modelBuilder.Entity<GoalWorkouts>()
                .HasKey(wg => new { wg.WorkoutId, wg.GoalId });
            modelBuilder.Entity<GoalWorkouts>()
                .Property<bool>("Completed");
            modelBuilder.Entity<GoalWorkouts>()
                .HasOne(wg => wg.Workout)
                .WithMany(w => w.WorkoutGoals)
                .HasForeignKey(wg => wg.WorkoutId);
            modelBuilder.Entity<GoalWorkouts>()
                .HasOne(wg => wg.Goal)
                .WithMany(g => g.WorkoutGoals)
                .HasForeignKey(wg => wg.GoalId);


            modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            {
                Completed = false,
                WorkoutId = 1,
                GoalId = 1,
            });

            modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            {
                Completed = false,
                WorkoutId = 2,
                GoalId = 2,
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
