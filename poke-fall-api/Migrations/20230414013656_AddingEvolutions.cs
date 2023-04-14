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
            using (var pokemonSpeciesReader = new StreamReader(@"./data/pokemon_species.csv"))
            using (var pokemonItemsReader = new StreamReader(@"./data/items.csv"))
            using (var pokmeonEvolutionCsv = new CsvReader(pokemonEvolutionReader, CultureInfo.InvariantCulture))
            {
                var pokemonEvolutionArray = pokmeonEvolutionCsv.GetRecords<PokeApiEvolution>().ToArray();
                using (var pokemonItemsCsv = new CsvReader(pokemonItemsReader, CultureInfo.InvariantCulture))
                {
                    var pokemonItemsArray = pokemonItemsCsv.GetRecords<PokeApiItem>().ToArray();
                    using (var pokmeonSpeciesCsv = new CsvReader(pokemonSpeciesReader, CultureInfo.InvariantCulture))
                    {
                        var pokemonSpeciesArray = pokmeonSpeciesCsv.GetRecords<PokeApiSpecies>().ToArray();
                        int count = 1;
                        foreach (PokeApiEvolution pokeApiEvolution in pokemonEvolutionArray)
                        {
                            foreach (PokeApiSpecies pokeSpecies in pokemonSpeciesArray)
                            {
                                if (pokeApiEvolution.evolved_species_id == pokeSpecies.id)
                                {
                                    Evolution tempEvolution = new Evolution
                                    {
                                        Id = count++,
                                        EvolvedPokedexNumber = pokeApiEvolution.evolved_species_id,
                                        EvolvedFromPokedexNumber = pokeSpecies.evolves_from_species_id,
                                        EvolutionTrigger = new EvolutionTriggerMap().getEvoTriggerMap()[pokeApiEvolution.evolution_trigger_id],
                                        TriggerItem = "",
                                        MinLevel = pokeApiEvolution.minimum_level,
                                        Gender = new GenderMap().getGenderMap()[pokeApiEvolution.gender_id],
                                        Location = pokeApiEvolution.location_id.ToString(),
                                        HeldItem = "",
                                        TimeOfDay = pokeApiEvolution.time_of_day,
                                        KnownMoveId = pokeApiEvolution.known_move_id,
                                        KnownMoveType = new PokemonTypeMap().getTypeMap()[pokeApiEvolution.known_move_type_id],
                                        MinBeauty = pokeApiEvolution.minimum_beauty,
                                        MinAffection = pokeApiEvolution.minimum_affection,
                                        RelativePhysicalStats = pokeApiEvolution.relative_physical_stats,
                                        PartyPokedexNumber = pokeApiEvolution.party_species_id,
                                        PartyType = new PokemonTypeMap().getTypeMap()[pokeApiEvolution.party_type_id],
                                        TradePokedexNumber = pokeApiEvolution.trade_species_id,
                                        NeedsOverworldRain = pokeApiEvolution.needs_overworld_rain != 0,
                                        TurnUpsideDown = pokeApiEvolution.turn_upside_down != 0,
                                    };

                                    if (pokeApiEvolution.trigger_item_id != 0 || pokeApiEvolution.held_item_id != 0)
                                    {
                                        foreach (PokeApiItem pokeItem in pokemonItemsArray)
                                        {
                                            //trigger item
                                            if (pokeItem.id == pokeApiEvolution.trigger_item_id)
                                            {
                                                tempEvolution.TriggerItem = pokeItem.identifier;
                                            }
                                            //held item
                                            else if (pokeItem.id == pokeApiEvolution.held_item_id)
                                            {
                                                tempEvolution.HeldItem = pokeItem.identifier;
                                            }
                                        }
                                    }
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
