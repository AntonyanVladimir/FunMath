﻿// <auto-generated />
using System;
using FunMath.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunMath.Migrations
{
    [DbContext(typeof(TaskContext))]
    partial class TaskContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("FunMath.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("FunMath.Models.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChallengeText")
                        .HasColumnType("TEXT");

                    b.Property<int>("LevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Solution")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("FunMath.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LevelNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("FunMath.Models.Challenge", b =>
                {
                    b.HasOne("FunMath.Models.Level", "Level")
                        .WithMany("Challenges")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("FunMath.Models.Level", b =>
                {
                    b.Navigation("Challenges");
                });
#pragma warning restore 612, 618
        }
    }
}
