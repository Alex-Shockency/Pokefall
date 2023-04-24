using System.ComponentModel;
using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Enums;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moves",
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
                        Description = table.Column<string>(type: "text", nullable: false),
                        Type = table.Column<int>(type: "text", nullable: false),
                        Category = table.Column<string>(type: "text", nullable: false),
                        PP = table.Column<int>(type: "integer", nullable: false),
                        Power = table.Column<int>(type: "integer", nullable: false),
                        Accuracy = table.Column<int>(type: "integer", nullable: false),
                        Contact = table.Column<bool>(type: "boolean", nullable: false),
                        AffectedProtect = table.Column<bool>(type: "boolean", nullable: false),
                        AffectedSnatch = table.Column<bool>(type: "boolean", nullable: false),
                        AffectedMirrorMove = table.Column<bool>(type: "boolean", nullable: false),
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                }
            );
            var databaseMoves = new List<Move>();
            using (var moveReader = new StreamReader(@"./data/moves.csv"))
            using (var flavorReader = new StreamReader(@"./data/move_flavor_text.csv"))
            using (var flavorCsv = new CsvReader(flavorReader, CultureInfo.InvariantCulture))
            {
                using (var moveCsv = new CsvReader(moveReader, CultureInfo.InvariantCulture))
                {
                    var moves = moveCsv.GetRecords<PokeApiMove>().ToArray();
                    
                    Dictionary<Int32, string> damageTypeMap = new Dictionary<int, string>();
                    damageTypeMap.Add(1, "Status");
                    damageTypeMap.Add(2, "Physical");
                    damageTypeMap.Add(3, "Special");

                    Dictionary<Int32, string> typeMap = new PokemonTypeMap().getTypeMap();
                    

                    var flavorTexts = flavorCsv.GetRecords<PokeApiMoveFlavorText>().ToArray();
                    foreach (PokeApiMove move in moves)
                    {
                        foreach (PokeApiMoveFlavorText text in flavorTexts)
                        {
                            if (
                                move.id == text.move_id
                                && text.version_group_id == 20
                                && text.language_id == 9
                            )
                            {
                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                var moveName = textInfo.ToTitleCase(
                                    move.identifier.Replace("-", " ")
                                );
                                databaseMoves.Add(
                                    new Move
                                    {
                                        Id = move.id,
                                        Name = moveName,
                                        Description = text.flavor_text,
                                        Type = typeMap[move.type_id],
                                        Category = damageTypeMap[move.damage_class_id],
                                        PP = move.pp,
                                        Power = move.power,
                                        Accuracy = move.accuracy,
                                        Contact = false,
                                        AffectedProtect = false,
                                        AffectedSnatch = false,
                                        AffectedMirrorMove = false,
                                    }
                                );
                            }
                        }
                    }
                }
            }

            using (var moveFlagReader = new StreamReader(@"./data/move_flag_map.csv"))
            {
                using (
                    var moveFlagCsv = new CsvReader(moveFlagReader, CultureInfo.InvariantCulture)
                )
                {
                    var moveFlags = moveFlagCsv.GetRecords<PokeApiMoveFlag>().ToArray();
                    foreach (PokeApiMoveFlag moveFlag in moveFlags)
                    {
                        switch (moveFlag.move_flag_id)
                        {
                            case 1:
                                databaseMoves[moveFlag.move_id - 1].Contact = true;
                                break;
                            case 4:
                                databaseMoves[moveFlag.move_id - 1].AffectedProtect = true;
                                break;
                            case 6:
                                databaseMoves[moveFlag.move_id - 1].AffectedSnatch = true;
                                break;
                            case 7:
                                databaseMoves[moveFlag.move_id - 1].AffectedMirrorMove = true;
                                break;
                        }
                    }
                }
            }

            foreach (Move move in databaseMoves)
            {
                migrationBuilder.InsertData(
                    table: "Moves",
                    columns: new[]
                    {
                        "Id",
                        "Name",
                        "Description",
                        "Type",
                        "Category",
                        "PP",
                        "Power",
                        "Accuracy",
                        "Contact",
                        "AffectedProtect",
                        "AffectedSnatch",
                        "AffectedMirrorMove"
                    },
                    values: new object[]
                    {
                        move.Id,
                        move.Name,
                        move.Description,
                        move.Type,
                        move.Category,
                        move.PP,
                        move.Power,
                        move.Accuracy,
                        move.Contact,
                        move.AffectedProtect,
                        move.AffectedSnatch,
                        move.AffectedMirrorMove
                    }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }

        private class PokeApiMove
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public int generation_id { get; set; }
            public int type_id { get; set; }
            public int power { get; set; } = 0;
            public int pp { get; set; }
            public int accuracy { get; set; }
            public int priority { get; set; }
            public int target_id { get; set; }
            public int damage_class_id { get; set; }
            public int effect_id { get; set; }
            public int effect_chance { get; set; } = 0;
            public int contest_type_id { get; set; }
            public int contest_effect_id { get; set; }
            public int super_contest_effect_id { get; set; }
        }

        private class PokeApiMoveFlavorText
        {
            public int move_id { get; set; }
            public int version_group_id { get; set; }
            public int language_id { get; set; }
            public string flavor_text { get; set; }
        }

        private class PokeApiMoveFlag
        {
            public int move_id { get; set; }
            public int move_flag_id { get; set; }
        }
    }
}
