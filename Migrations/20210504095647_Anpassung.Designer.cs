﻿// <auto-generated />
using FunMath.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunMath.Migrations
{
    [DbContext(typeof(TaskContext))]
    [Migration("20210504095647_Anpassung")]
    partial class Anpassung
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FunMath.Models.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChallengeText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int>("LevelNumber")
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("FunMath.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LevelNumber")
                        .HasColumnType("int");

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
