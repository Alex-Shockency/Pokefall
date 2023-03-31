using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table =>
                    new
                    {
                        PokemonId = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        PokedexNumber = table.Column<int>(type: "integer", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Type1 = table.Column<string>(type: "text", nullable: false),
                        Type2 = table.Column<string>(type: "text", nullable: false),
                        Ability1Id = table.Column<int>(type: "integer", nullable: false),
                        Ability2Id = table.Column<int>(type: "integer", nullable: false),
                        HiddenAbilityId = table.Column<int>(type: "integer", nullable: false),
                        // CatchRate = table.Column<int>(type: "integer", nullable: false),
                        // EggGroup = table.Column<int>(type: "integer", nullable: false),
                        // HatchStepsMin = table.Column<int>(type: "integer", nullable: false),
                        // HatchStepMax = table.Column<int>(type: "integer", nullable: false),
                        Height = table.Column<int>(type: "integer", nullable: false),
                        Weight = table.Column<int>(type: "integer", nullable: false),
                        BaseEXPYield = table.Column<int>(type: "integer", nullable: false),
                        HP = table.Column<int>(type: "integer", nullable: false),
                        Attack = table.Column<int>(type: "integer", nullable: false),
                        Defense = table.Column<int>(type: "integer", nullable: false),
                        SpecAttack = table.Column<int>(type: "integer", nullable: false),
                        SpecDefense = table.Column<int>(type: "integer", nullable: false),
                        Speed = table.Column<int>(type: "integer", nullable: false),
                        // LevelingRate = table.Column<string>(type: "text", nullable: false),
                        // BaseFriendship = table.Column<int>(type: "integer", nullable: false),
                        // EvolvesTo = table.Column<int>(type: "integer", nullable: false),
                        // EvolvesFrom = table.Column<int>(type: "integer", nullable: false),
                        // EvolutionDescription = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.PokemonId);
                }
            );

            var databasePokemon = new List<Pokemon>();
            Dictionary<Int32, string> typeMap = new PokemonTypeMap().getTypeMap();
            using (var pokemonReader = new StreamReader(@"./data/pokemon.csv"))
            using (var pokemonTypeReader = new StreamReader(@"./data/pokemon_types.csv"))
            using (var pokemonAbilitiesReader = new StreamReader(@"./data/pokemon_abilities.csv"))
            using (var pokemonStatsReader = new StreamReader(@"./data/pokemon_stats.csv"))
            using (var pokmeonCsv = new CsvReader(pokemonReader, CultureInfo.InvariantCulture))
            {
                var pokemonArray = pokmeonCsv.GetRecords<PokeApiPokemon>().ToArray();
                using (
                    var pokmeonTypeCsv = new CsvReader(
                        pokemonTypeReader,
                        CultureInfo.InvariantCulture
                    )
                )
                {
                    using (
                        var pokemonAbilitiesCsv = new CsvReader(
                            pokemonAbilitiesReader,
                            CultureInfo.InvariantCulture
                        )
                    )
                    {
                        using (
                            var pokemonStatsCsv = new CsvReader(
                                pokemonStatsReader,
                                CultureInfo.InvariantCulture
                            )
                        )
                        {
                            var pokemonAbilities = pokemonAbilitiesCsv
                                .GetRecords<PokeApiPokemonAbility>()
                                .ToArray();
                            var pokemonTypeArray = pokmeonTypeCsv
                                .GetRecords<PokeApiPokemonType>()
                                .ToArray();
                            var pokemonStatArray = pokemonStatsCsv
                                .GetRecords<PokeApiPokemonStat>()
                                .ToArray();
                            foreach (PokeApiPokemon pokemon in pokemonArray)
                            {
                                int hp = 0,
                                    attack = 0,
                                    defense = 0,
                                    specAttack = 0,
                                    specDefense = 0,
                                    speed = 0;
                                foreach (PokeApiPokemonStat pokemonStat in pokemonStatArray)
                                {
                                    if (pokemon.id == pokemonStat.pokemon_id)
                                    {
                                        switch (pokemonStat.stat_id)
                                        {
                                            case 1:
                                                hp = pokemonStat.base_stat;
                                                break;
                                            case 2:
                                                attack = pokemonStat.base_stat;
                                                break;
                                            case 3:
                                                defense = pokemonStat.base_stat;
                                                break;
                                            case 4:
                                                specAttack = pokemonStat.base_stat;
                                                break;
                                            case 5:
                                                specDefense = pokemonStat.base_stat;
                                                break;
                                            case 6:
                                                speed = pokemonStat.base_stat;
                                                break;
                                        }
                                    }
                                }
                                int ability1Id = 0;
                                int ability2Id = 0;
                                int hiddenAbilityId = 0;
                                foreach (PokeApiPokemonAbility pokemonAbility in pokemonAbilities)
                                {
                                    if (
                                        pokemon.id == pokemonAbility.pokemon_id
                                        && pokemonAbility.slot == 1
                                        && !pokemonAbility.is_hidden
                                    )
                                    {
                                        ability1Id = pokemonAbility.ability_id;
                                    }
                                    else if (
                                        pokemon.id == pokemonAbility.pokemon_id
                                        && pokemonAbility.slot == 2
                                        && !pokemonAbility.is_hidden
                                    )
                                    {
                                        ability2Id = pokemonAbility.ability_id;
                                    }
                                    else if (
                                        pokemon.id == pokemonAbility.pokemon_id
                                        && pokemonAbility.is_hidden
                                    )
                                    {
                                        hiddenAbilityId = pokemonAbility.ability_id;
                                    }
                                }
                                String type1 = "";
                                String type2 = "";
                                foreach (PokeApiPokemonType pokemonType in pokemonTypeArray)
                                {
                                    if (
                                        pokemon.id == pokemonType.pokemon_id
                                        && pokemonType.slot == 1
                                    )
                                    {
                                        type1 = typeMap[pokemonType.type_id].ToString();
                                    }
                                    else if (
                                        pokemon.id == pokemonType.pokemon_id
                                        && pokemonType.slot == 2
                                    )
                                    {
                                        type2 = typeMap[pokemonType.type_id].ToString();
                                    }
                                }

                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                var pokemonName = textInfo.ToTitleCase(pokemon.identifier);
                                databasePokemon.Add(
                                    new Pokemon
                                    {
                                        PokemonId = pokemon.id,
                                        PokedexNumber = pokemon.species_id,
                                        Name = pokemonName,
                                        Type1 = type1,
                                        Type2 = type2,
                                        Ability1Id = ability1Id,
                                        Ability2Id = ability2Id,
                                        HiddenAbilityId = hiddenAbilityId,
                                        Height = pokemon.height,
                                        Weight = pokemon.weight,
                                        BaseEXPYield = pokemon.base_experience,
                                        HP = hp,
                                        Attack = attack,
                                        Defense = defense,
                                        SpecAttack = specAttack,
                                        SpecDefense = specDefense,
                                        Speed = speed,
                                    }
                                );
                            }
                        }
                    }
                }
            }

            foreach (Pokemon pokemon in databasePokemon)
            {
                migrationBuilder.InsertData(
                    table: "Pokemon",
                    columns: new[]
                    {
                        "PokemonId",
                        "PokedexNumber",
                        "Name",
                        "Type1",
                        "Type2",
                        "Ability1Id",
                        "Ability2Id",
                        "HiddenAbilityId",
                        "Height",
                        "Weight",
                        "BaseEXPYield",
                        "HP",
                        "Attack",
                        "Defense",
                        "SpecAttack",
                        "SpecDefense",
                        "Speed",
                    },
                    values: new object[]
                    {
                        pokemon.PokemonId,
                        pokemon.PokedexNumber,
                        pokemon.Name,
                        pokemon.Type1,
                        pokemon.Type2,
                        pokemon.Ability1Id,
                        pokemon.Ability2Id,
                        pokemon.HiddenAbilityId,
                        pokemon.Height,
                        pokemon.Weight,
                        pokemon.BaseEXPYield,
                        pokemon.HP,
                        pokemon.Attack,
                        pokemon.Defense,
                        pokemon.SpecAttack,
                        pokemon.SpecDefense,
                        pokemon.Speed,
                    }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }

        private class PokeApiPokemon
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public int species_id { get; set; }
            public int height { get; set; }
            public int weight { get; set; } = 0;
            public int base_experience { get; set; }
            public int order { get; set; }
            public int is_default { get; set; }
        }

        private class PokeApiPokemonType
        {
            public int pokemon_id { get; set; }
            public int type_id { get; set; }
            public int slot { get; set; }
        }

        private class PokeApiPokemonAbility
        {
            public int pokemon_id { get; set; }
            public int ability_id { get; set; }
            public bool is_hidden { get; set; }
            public int slot { get; set; }
        }

        private class PokeApiPokemonStat
        {
            public int pokemon_id { get; set; }
            public int stat_id { get; set; }
            public int base_stat { get; set; }
            public int effort { get; set; }
        }
    }
}
