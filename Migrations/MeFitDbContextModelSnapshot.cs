// <auto-generated />
using System;
using MeFit.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeFit.Migrations
{
    [DbContext(typeof(MeFitDbContext))]
    partial class MeFitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeFit.Models.Domain.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TargetMuscleGroup")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VideoURL")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Biceps strong",
                            ImageURL = "img",
                            Name = "Barbell curl",
                            TargetMuscleGroup = "biceps",
                            VideoURL = ".mov"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Legs strong",
                            ImageURL = "img",
                            Name = "Leg press",
                            TargetMuscleGroup = "legs",
                            VideoURL = ".mov"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Chest strong",
                            ImageURL = "img",
                            Name = "Push up",
                            TargetMuscleGroup = "upper body",
                            VideoURL = ".mov"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Biceps strong",
                            ImageURL = "img",
                            Name = "Isolation curl",
                            TargetMuscleGroup = "biceps",
                            VideoURL = ".mov"
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Achieved")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ProgramEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("ProgramId");

                    b.ToTable("Goals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Achieved = false,
                            ProfileId = "keycloak-uid",
                            ProgramEndDate = new DateTime(2022, 3, 24, 9, 26, 44, 291, DateTimeKind.Local).AddTicks(9924),
                            ProgramId = 1
                        },
                        new
                        {
                            Id = 2,
                            Achieved = true,
                            ProfileId = "keycloak-uid",
                            ProgramEndDate = new DateTime(2022, 3, 24, 9, 26, 44, 294, DateTimeKind.Local).AddTicks(7363),
                            ProgramId = 2
                        },
                        new
                        {
                            Id = 3,
                            Achieved = true,
                            ProfileId = "keycloak-uid",
                            ProgramEndDate = new DateTime(2022, 3, 24, 9, 26, 44, 294, DateTimeKind.Local).AddTicks(7412),
                            ProgramId = 2
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.GoalWorkouts", b =>
                {
                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int>("GoalId")
                        .HasColumnType("int");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.HasKey("WorkoutId", "GoalId");

                    b.HasIndex("GoalId");

                    b.ToTable("GoalWorkouts");

                    b.HasData(
                        new
                        {
                            WorkoutId = 1,
                            GoalId = 1,
                            Completed = false
                        },
                        new
                        {
                            WorkoutId = 2,
                            GoalId = 2,
                            Completed = false
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.MFProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Programs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Upper body",
                            Name = "Bicep enhancement"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Whole body",
                            Name = "Strength building"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Whole body",
                            Name = "Cardio"
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.Profile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Disabilities")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<string>("MedicalConditions")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = "keycloak-uid",
                            Disabilities = "none",
                            Height = 171,
                            MedicalConditions = "Anxiety",
                            Weight = 60
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseRepetitions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Sets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExerciseId = 1,
                            ExerciseRepetitions = 10
                        },
                        new
                        {
                            Id = 2,
                            ExerciseId = 2,
                            ExerciseRepetitions = 20
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Workouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Arm day",
                            Type = "Strength"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Leg day",
                            Type = "Strength"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Running",
                            Type = "Cardio"
                        });
                });

            modelBuilder.Entity("WorkoutPrograms", b =>
                {
                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("ProgramId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutPrograms");

                    b.HasData(
                        new
                        {
                            ProgramId = 1,
                            WorkoutId = 1
                        },
                        new
                        {
                            ProgramId = 2,
                            WorkoutId = 1
                        },
                        new
                        {
                            ProgramId = 2,
                            WorkoutId = 2
                        },
                        new
                        {
                            ProgramId = 3,
                            WorkoutId = 3
                        });
                });

            modelBuilder.Entity("WorkoutSets", b =>
                {
                    b.Property<int>("SetId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("SetId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutSets");

                    b.HasData(
                        new
                        {
                            SetId = 1,
                            WorkoutId = 1
                        },
                        new
                        {
                            SetId = 2,
                            WorkoutId = 1
                        },
                        new
                        {
                            SetId = 1,
                            WorkoutId = 2
                        });
                });

            modelBuilder.Entity("MeFit.Models.Domain.Goal", b =>
                {
                    b.HasOne("MeFit.Models.Domain.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("MeFit.Models.Domain.MFProgram", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId");

                    b.Navigation("Profile");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("MeFit.Models.Domain.GoalWorkouts", b =>
                {
                    b.HasOne("MeFit.Models.Domain.Goal", "Goal")
                        .WithMany("WorkoutGoals")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFit.Models.Domain.Workout", "Workout")
                        .WithMany("WorkoutGoals")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("MeFit.Models.Domain.Set", b =>
                {
                    b.HasOne("MeFit.Models.Domain.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("WorkoutPrograms", b =>
                {
                    b.HasOne("MeFit.Models.Domain.MFProgram", null)
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFit.Models.Domain.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutSets", b =>
                {
                    b.HasOne("MeFit.Models.Domain.Set", null)
                        .WithMany()
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFit.Models.Domain.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeFit.Models.Domain.Goal", b =>
                {
                    b.Navigation("WorkoutGoals");
                });

            modelBuilder.Entity("MeFit.Models.Domain.Workout", b =>
                {
                    b.Navigation("WorkoutGoals");
                });
#pragma warning restore 612, 618
        }
    }
}
