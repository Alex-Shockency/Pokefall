﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    [DbContext(typeof(PokefallContext))]
    partial class PokefallContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("poke_fall_api.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Accuracy")
                        .HasColumnType("integer");

                    b.Property<bool>("AffectedKingsRock")
                        .HasColumnType("boolean");

                    b.Property<bool>("AffectedMagicCoat")
                        .HasColumnType("boolean");

                    b.Property<bool>("AffectedMirrorMove")
                        .HasColumnType("boolean");

                    b.Property<bool>("AffectedProtect")
                        .HasColumnType("boolean");

                    b.Property<bool>("AffectedSnatch")
                        .HasColumnType("boolean");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Contact")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PP")
                        .HasColumnType("integer");

                    b.Property<int?>("PokemonId")
                        .HasColumnType("integer");

                    b.Property<int?>("PokemonId1")
                        .HasColumnType("integer");

                    b.Property<int?>("PokemonId2")
                        .HasColumnType("integer");

                    b.Property<int>("Power")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("PokemonId1");

                    b.HasIndex("PokemonId2");

                    b.ToTable("Move");
                });

            modelBuilder.Entity("poke_fall_api.Models.Pokemon", b =>
                {
                    b.Property<int>("PokemonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PokemonId"));

                    b.Property<int>("Ability1Id")
                        .HasColumnType("integer");

                    b.Property<int>("Ability2Id")
                        .HasColumnType("integer");

                    b.Property<int>("BaseEXPYield")
                        .HasColumnType("integer");

                    b.Property<int>("BaseFriendship")
                        .HasColumnType("integer");

                    b.Property<int>("CatchRate")
                        .HasColumnType("integer");

                    b.Property<int>("EggGroup")
                        .HasColumnType("integer");

                    b.Property<string>("EvolutionDescription")
                        .HasColumnType("text");

                    b.Property<int>("EvolvesFrom")
                        .HasColumnType("integer");

                    b.Property<int>("EvolvesTo")
                        .HasColumnType("integer");

                    b.Property<int>("HatchStepMax")
                        .HasColumnType("integer");

                    b.Property<int>("HatchStepsMin")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("HiddenAbilityId")
                        .HasColumnType("integer");

                    b.Property<string>("LevelingRate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PokedexNumber")
                        .HasColumnType("integer");

                    b.Property<int>("Type1")
                        .HasColumnType("integer");

                    b.Property<int>("Type2")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("PokemonId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("poke_fall_api.Models.Move", b =>
                {
                    b.HasOne("poke_fall_api.Models.Pokemon", null)
                        .WithMany("EggMoves")
                        .HasForeignKey("PokemonId");

                    b.HasOne("poke_fall_api.Models.Pokemon", null)
                        .WithMany("TMMoves")
                        .HasForeignKey("PokemonId1");

                    b.HasOne("poke_fall_api.Models.Pokemon", null)
                        .WithMany("TutorMoves")
                        .HasForeignKey("PokemonId2");
                });

            modelBuilder.Entity("poke_fall_api.Models.Pokemon", b =>
                {
                    b.Navigation("EggMoves");

                    b.Navigation("TMMoves");

                    b.Navigation("TutorMoves");
                });
#pragma warning restore 612, 618
        }
    }
}
