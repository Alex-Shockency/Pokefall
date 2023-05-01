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

            modelBuilder.Entity("PokemonMove", b =>
                {
                    b.Property<bool>("EggMoveLearnable")
                        .HasColumnType("boolean");

                    b.Property<int?>("LevelLearned")
                        .HasColumnType("integer");

                    b.Property<bool>("LevelUpLearnable")
                        .HasColumnType("boolean");

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.Property<bool>("TMLearnable")
                        .HasColumnType("boolean");

                    b.Property<bool>("TutorLearnable")
                        .HasColumnType("boolean");

                    b.ToTable("PokemonMoves");
                });

            modelBuilder.Entity("poke_fall_api.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("poke_fall_api.Models.EggMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("EggMoves");
                });

            modelBuilder.Entity("poke_fall_api.Models.Evolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EvolutionChainId")
                        .HasColumnType("integer");

                    b.Property<string>("EvolutionTrigger")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EvolvedFromPokedexNumber")
                        .HasColumnType("integer");

                    b.Property<int>("EvolvedPokedexNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HeldItem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("KnownMoveId")
                        .HasColumnType("integer");

                    b.Property<string>("KnownMoveType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinAffection")
                        .HasColumnType("integer");

                    b.Property<int>("MinBeauty")
                        .HasColumnType("integer");

                    b.Property<int>("MinHappiness")
                        .HasColumnType("integer");

                    b.Property<int>("MinLevel")
                        .HasColumnType("integer");

                    b.Property<bool>("NeedsOverworldRain")
                        .HasColumnType("boolean");

                    b.Property<int>("PartyPokedexNumber")
                        .HasColumnType("integer");

                    b.Property<string>("PartyType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RelativePhysicalStats")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TimeOfDay")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TradePokedexNumber")
                        .HasColumnType("integer");

                    b.Property<string>("TriggerItem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TurnUpsideDown")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Evolutions");
                });

            modelBuilder.Entity("poke_fall_api.Models.LevelUpMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LevelUpMoves");
                });

            modelBuilder.Entity("poke_fall_api.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Accuracy")
                        .HasColumnType("integer");

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

                    b.Property<int>("Power")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("poke_fall_api.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Ability1Id")
                        .HasColumnType("integer");

                    b.Property<int?>("Ability2Id")
                        .HasColumnType("integer");

                    b.Property<int>("Attack")
                        .HasColumnType("integer");

                    b.Property<int>("BaseEXPYield")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int?>("EvolutionChainId")
                        .HasColumnType("integer");

                    b.Property<int>("HP")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("HiddenAbilityId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBaby")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PokedexNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SpecAttack")
                        .HasColumnType("integer");

                    b.Property<int>("SpecDefense")
                        .HasColumnType("integer");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<string>("Type1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("poke_fall_api.Models.TMMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TMMoves");
                });

            modelBuilder.Entity("poke_fall_api.Models.TutorMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TutorMoves");
                });
#pragma warning restore 612, 618
        }
    }
}
