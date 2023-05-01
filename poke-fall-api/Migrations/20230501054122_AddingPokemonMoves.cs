using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddingPokemonMoves : Migration
    {
        readonly string[] POKEMON_MOVES_COLUMNS = new string[]
        {
            "PokemonId",
            "MoveId",
            "VersionId",
            "LevelUpLearnable",
            "LevelLearned",
            "TMLearnable",
            "TutorLearnable",
            "EggMoveLearnable"
        };
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonMoves",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false),
                    VersionId = table.Column<int>(type: "integer", nullable: false),
                    LevelUpLearnable = table.Column<bool>(type: "boolean", nullable: false),
                    LevelLearned = table.Column<int>(type: "integer", nullable: true),
                    TMLearnable = table.Column<bool>(type: "boolean", nullable: false),
                    TutorLearnable = table.Column<bool>(type: "boolean", nullable: false),
                    EggMoveLearnable = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                });
            seedPokemonMoves(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                          name: "PokemonMoves");
        }

        private void seedPokemonMoves(MigrationBuilder migrationbuilder)
        {
            var insertMoves = new List<PokemonMove>();
            using (var moveReader = new StreamReader(@"./data/learned_moves.csv"))
            using (var moveCsv = new CsvReader(moveReader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var moves = moveCsv.GetRecords<PokemonMove>().ToArray();
                foreach (PokemonMove move in moves)
                {
                    migrationbuilder.InsertData(
                        table: "PokemonMoves",
                        columns: POKEMON_MOVES_COLUMNS,
                        values: new object[]
                        {
                                move.PokemonId,
                                move.MoveId,
                                move.VersionId,
                                move.LevelUpLearnable,
                                move.LevelLearned,
                                move.TMLearnable,
                                move.TutorLearnable,
                                move.EggMoveLearnable,
                        }
                    );
                }
            }
        }
    }
}
