using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingEvolutions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evolutions",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        EvolutionChainId = table.Column<int>(type: "integer", nullable: true),
                        EvolvedPokedexNumber = table.Column<int>(type: "integer", nullable: false),
                        EvolvedFromPokedexNumber = table.Column<int>(type: "integer", nullable: false),
                        EvolutionTrigger = table.Column<string>(type: "text", nullable: false),
                        TriggerItem = table.Column<string>(type: "text", nullable: true),
                        MinLevel = table.Column<string>(type: "integer", nullable: true),
                        Gender = table.Column<int>(type: "text", nullable: true),
                        Location = table.Column<int>(type: "text", nullable: true),
                        HeldItem = table.Column<int>(type: "text", nullable: true),
                        TimeOfDay = table.Column<int>(type: "text", nullable: true),
                        KnownMoveId = table.Column<int>(type: "integer", nullable: true),
                        KnownMoveType = table.Column<int>(type: "text", nullable: true),
                        MinHappiness = table.Column<int>(type: "integer", nullable: true),
                        MinBeauty = table.Column<int>(type: "integer", nullable: true),
                        MinAffection = table.Column<int>(type: "integer", nullable: true),
                        RelativePhysicalStats = table.Column<int>(type: "text", nullable: true),
                        PartyPokedexNumber = table.Column<int>(type: "integer", nullable: true),
                        PartyType = table.Column<int>(type: "text", nullable: true),
                        TradePokedexNumber = table.Column<int>(type: "integer", nullable: true),
                        NeedsOverworldRain = table.Column<int>(type: "boolean", nullable: true),
                        TurnUpsideDown = table.Column<int>(type: "boolean", nullable: true),
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolutions", x => x.Id);
                }
            );

            var databaseEvolution = new Dictionary<int, Evolution>();
            Dictionary<Int32, string> evolutionTriggerMap = new EvolutionTriggerMap().getEvoTriggerMap();
            using (var pokemonEvolutionReader = new StreamReader(@"./data/pokemon_evolution.csv"))
            using (var evolutionsReader = new StreamReader(@"./data/evolutions.csv"))
            using (var pokemonSpeciesReader = new StreamReader(@"./data/pokemon_species.csv"))
            using (var pokemonItemsReader = new StreamReader(@"./data/items.csv"))
            using (var pokmeonEvolutionCsv = new CsvReader(evolutionsReader, CultureInfo.InvariantCulture))
            {
                var pokemonEvolutionArray = pokmeonEvolutionCsv.GetRecords<Evolution>().ToArray();
                using (var pokemonItemsCsv = new CsvReader(pokemonItemsReader, CultureInfo.InvariantCulture))
                {
                    var pokemonItemsArray = pokemonItemsCsv.GetRecords<PokeApiItem>().ToArray();
                    using (var pokmeonSpeciesCsv = new CsvReader(pokemonSpeciesReader, CultureInfo.InvariantCulture))
                    {
                        var pokemonSpeciesArray = pokmeonSpeciesCsv.GetRecords<PokeApiSpecies>().ToArray();
                        int count = 1;
                        foreach (Evolution pokeApiEvolution in pokemonEvolutionArray)
                        {
                            foreach (PokeApiSpecies pokeSpecies in pokemonSpeciesArray)
                            {
                                if (pokeApiEvolution.EvolvedPokedexNumber == pokeSpecies.id)
                                {
                                    Evolution tempEvolution = new Evolution
                                    {
                                        Id = count++,
                                        EvolutionChainId = pokeApiEvolution.EvolutionChainId,
                                        EvolvedPokedexNumber = pokeApiEvolution.EvolvedPokedexNumber,
                                        EvolvedFromPokedexNumber = pokeSpecies.evolves_from_species_id,
                                        EvolutionTrigger = pokeApiEvolution.EvolutionTrigger,
                                        TriggerItem = pokeApiEvolution.TriggerItem,
                                        MinLevel = pokeApiEvolution.MinLevel,
                                        Gender = pokeApiEvolution.Gender,
                                        Location = pokeApiEvolution.Location.ToString(),
                                        HeldItem = pokeApiEvolution.HeldItem,
                                        TimeOfDay = pokeApiEvolution.TimeOfDay,
                                        KnownMoveId = pokeApiEvolution.KnownMoveId,
                                        KnownMoveType = pokeApiEvolution.KnownMoveType,
                                        MinHappiness = pokeApiEvolution.MinHappiness,
                                        MinBeauty = pokeApiEvolution.MinBeauty,
                                        MinAffection = pokeApiEvolution.MinAffection,
                                        RelativePhysicalStats = pokeApiEvolution.RelativePhysicalStats,
                                        PartyPokedexNumber = pokeApiEvolution.PartyPokedexNumber,
                                        PartyType = pokeApiEvolution.PartyType,
                                        TradePokedexNumber = pokeApiEvolution.TradePokedexNumber,
                                        NeedsOverworldRain = pokeApiEvolution.NeedsOverworldRain,
                                        TurnUpsideDown = pokeApiEvolution.TurnUpsideDown,
                                    };

                                    // if (pokeApiEvolution.trigger_item_id != 0 || pokeApiEvolution.held_item_id != 0)
                                    // {
                                    //     foreach (PokeApiItem pokeItem in pokemonItemsArray)
                                    //     {
                                    //         //trigger item
                                    //         if (pokeItem.id == pokeApiEvolution.trigger_item_id)
                                    //         {
                                    //             tempEvolution.TriggerItem = pokeItem.identifier;
                                    //         }
                                    //         //held item
                                    //         else if (pokeItem.id == pokeApiEvolution.held_item_id)
                                    //         {
                                    //             tempEvolution.HeldItem = pokeItem.identifier;
                                    //         }
                                    //     }
                                    // }
                                    databaseEvolution[count] = tempEvolution;
                                }
                            }
                        }
                    }
                }
            }

            foreach (int key in databaseEvolution.Keys)
            {
                migrationBuilder.InsertData(
                table: "Evolutions",
                columns: new[]
                {
                        "Id",
                        "EvolutionChainId",
                        "EvolvedPokedexNumber",
                        "EvolvedFromPokedexNumber",
                        "EvolutionTrigger",
                        "TriggerItem",
                        "MinLevel",
                        "Gender",
                        "Location",
                        "HeldItem",
                        "TimeOfDay",
                        "KnownMoveId",
                        "KnownMoveType",
                        "MinHappiness",
                        "MinBeauty",
                        "MinAffection",
                        "RelativePhysicalStats",
                        "PartyPokedexNumber",
                        "PartyType",
                        "TradePokedexNumber",
                        "NeedsOverworldRain",
                        "TurnUpsideDown",
                },
                values: new object[]
                {
                        databaseEvolution[key].Id,
                        databaseEvolution[key].EvolutionChainId,
                        databaseEvolution[key].EvolvedPokedexNumber,
                        databaseEvolution[key].EvolvedFromPokedexNumber,
                        databaseEvolution[key].EvolutionTrigger,
                        databaseEvolution[key].TriggerItem,
                        databaseEvolution[key].MinLevel,
                        databaseEvolution[key].Gender,
                        databaseEvolution[key].Location,
                        databaseEvolution[key].HeldItem,
                        databaseEvolution[key].TimeOfDay,
                        databaseEvolution[key].KnownMoveId,
                        databaseEvolution[key].KnownMoveType,
                        databaseEvolution[key].MinHappiness,
                        databaseEvolution[key].MinBeauty,
                        databaseEvolution[key].MinAffection,
                        databaseEvolution[key].RelativePhysicalStats,
                        databaseEvolution[key].PartyPokedexNumber,
                        databaseEvolution[key].PartyType,
                        databaseEvolution[key].TradePokedexNumber,
                        databaseEvolution[key].NeedsOverworldRain,
                        databaseEvolution[key].TurnUpsideDown,
                }
            );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }

        private class PokeApiEvolution
        {
            public int id { get; set; }
            public int evolved_species_id { get; set; }
            public int evolution_trigger_id { get; set; }
            public int trigger_item_id { get; set; }
            public int minimum_level { get; set; } = 0;
            public int gender_id { get; set; }
            public int location_id { get; set; }
            public int held_item_id { get; set; }
            public string time_of_day { get; set; }
            public int known_move_id { get; set; }
            public int known_move_type_id { get; set; }
            public int minimum_happiness { get; set; }
            public int minimum_beauty { get; set; }
            public int minimum_affection { get; set; }
            public string relative_physical_stats { get; set; }
            public int party_species_id { get; set; }
            public int party_type_id { get; set; }
            public int trade_species_id { get; set; }
            public int needs_overworld_rain { get; set; }
            public int turn_upside_down { get; set; }
        }

        private class PokeApiSpecies
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public int generation_id { get; set; }
            public int evolves_from_species_id { get; set; }
            public int evolution_chain_id { get; set; } = 0;
            public int color_id { get; set; }
            public int shape_id { get; set; }
            public int habitat_id { get; set; }
            public int gender_rate { get; set; }
            public int capture_rate { get; set; }
            public int base_happiness { get; set; }
            public int is_baby { get; set; }
            public int hatch_counter { get; set; }
            public int has_gender_differences { get; set; }
            public int growth_rate_id { get; set; }
            public int forms_switchable { get; set; }
            public int is_legendary { get; set; }
            public int is_mythical { get; set; }
            public int order { get; set; }
            public int conquest_order { get; set; }
        }

        private class PokeApiItem
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public int category_id { get; set; }
            public int cost { get; set; }
            public int fling_power { get; set; }
            public int fling_effect_id { get; set; }
        }
    }
}
