using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingAbilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                }
            );

            string[] columns = new string[] { "Id", "Name", "Description" };
            using (var abilReader = new StreamReader(@"./data/abilities.csv"))
            using (var abilCsv = new CsvReader(abilReader, CultureInfo.InvariantCulture))
            {
                var abilities = abilCsv.GetRecords<PokeApiAbility>().ToArray();
                using (
                    var flavorReader = new StreamReader(@"./data/flavortext.csv")
                )
                using (var flavorCsv = new CsvReader(flavorReader, CultureInfo.InvariantCulture))
                {
                    var flavorTexts = flavorCsv.GetRecords<PokeApiFlavorText>().ToArray();
                    foreach (PokeApiAbility ability in abilities)
                    {
                        foreach (PokeApiFlavorText text in flavorTexts)
                        {
                            if (
                                ability.id == text.ability_id
                                && text.version_group_id == 20
                                && text.language_id == 9
                            )
                            {
                                string[] dbValues = new string[]
                                {
                                    ability.id.ToString(),
                                    ability.identifier,
                                    text.flavor_text
                                };
                                migrationBuilder.InsertData("Abilities", columns, dbValues);
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Abilities");
        }

        private class PokeApiAbility
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public int generation_id { get; set; }
            public bool is_main_series { get; set; }
        }

        private class PokeApiFlavorText
        {
            public int ability_id { get; set; }
            public int version_group_id { get; set; }
            public int language_id { get; set; }
            public string flavor_text { get; set; }
        }
    }
}
