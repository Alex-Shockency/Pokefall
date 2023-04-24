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
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        PokedexNumber = table.Column<int>(type: "integer", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        EvolutionChainId = table.Column<int>(type: "integer", nullable: true),
                        Type1 = table.Column<string>(type: "text", nullable: false),
                        Type2 = table.Column<string>(type: "text", nullable: false),
                        Ability1Id = table.Column<int>(type: "integer", nullable: false),
                        Ability2Id = table.Column<int>(type: "integer", nullable: true),
                        HiddenAbilityId = table.Column<int>(type: "integer", nullable: false),
                        Height = table.Column<int>(type: "integer", nullable: false),
                        Weight = table.Column<int>(type: "integer", nullable: false),
                        BaseEXPYield = table.Column<int>(type: "integer", nullable: false),
                        HP = table.Column<int>(type: "integer", nullable: false),
                        Attack = table.Column<int>(type: "integer", nullable: false),
                        Defense = table.Column<int>(type: "integer", nullable: false),
                        SpecAttack = table.Column<int>(type: "integer", nullable: false),
                        SpecDefense = table.Column<int>(type: "integer", nullable: false),
                        Speed = table.Column<int>(type: "integer", nullable: false),
                        IsBaby = table.Column<bool>(type: "boolean", nullable: false),
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                }
            );

            var databasePokemon = new List<Pokemon>();
            Dictionary<Int32, string> typeMap = new PokemonTypeMap().getTypeMap();
            using (var pokemonReader = new StreamReader(@"./data/pokemon.csv"))
            using (var pokemonTypeReader = new StreamReader(@"./data/pokemon_types.csv"))
            using (var pokemonAbilitiesReader = new StreamReader(@"./data/pokemon_abilities.csv"))
            using (var pokemonStatsReader = new StreamReader(@"./data/pokemon_stats.csv"))
            using (var pokemonSpeciesReader = new StreamReader(@"./data/pokemon_species.csv"))
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
                            using (
                                var pokemonSpeciesCsv = new CsvReader(
                                    pokemonSpeciesReader,
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
                                var pokemonSpeciesArray = pokemonSpeciesCsv
                                    .GetRecords<PokeApiSpecies>()
                                    .ToArray();
                                int formChainId = 538;
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
                                    foreach (
                                        PokeApiPokemonAbility pokemonAbility in pokemonAbilities
                                    )
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
                                    int chainId = 0;
                                    bool isBaby = false;
                                    foreach (PokeApiSpecies pokemonSpecies in pokemonSpeciesArray)
                                    {
                                        if (pokemonSpecies.identifier.Equals(pokemon.identifier))
                                        {
                                            chainId = pokemonSpecies.evolution_chain_id;
                                            isBaby = pokemonSpecies.is_baby != 0;
                                        }
                                    }

                                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                    var pokemonName = textInfo.ToTitleCase(pokemon.identifier);
                                    databasePokemon.Add(
                                        new Pokemon
                                        {
                                            Id = pokemon.id,
                                            PokedexNumber = pokemon.species_id,
                                            Name = pokemonName,
                                            EvolutionChainId = chainId == 0 ? null : chainId,
                                            Type1 = type1,
                                            Type2 = type2,
                                            Ability1Id = ability1Id,
                                            Ability2Id = ability2Id == 0 ? null : ability2Id,
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
                                            IsBaby = isBaby,
                                        }
                                    );
                                }
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
                        "Id",
                        "PokedexNumber",
                        "Name",
                        "EvolutionChainId",
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
                        "IsBaby"
                    },
                    values: new object[]
                    {
                        pokemon.Id,
                        pokemon.PokedexNumber,
                        pokemon.Name,
                        pokemon.EvolutionChainId,
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
                        pokemon.IsBaby
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
    }
}
