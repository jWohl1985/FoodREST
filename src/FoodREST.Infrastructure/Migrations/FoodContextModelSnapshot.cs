﻿// <auto-generated />
using System;
using FoodREST.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodREST.Infrastructure.Migrations
{
    [DbContext(typeof(FoodContext))]
    partial class FoodContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodREST.Domain.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Calories")
                        .HasColumnType("integer");

                    b.Property<int>("CarbohydrateGrams")
                        .HasColumnType("integer");

                    b.Property<int>("FatGrams")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("ProteinGrams")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });
#pragma warning restore 612, 618
        }
    }
}
