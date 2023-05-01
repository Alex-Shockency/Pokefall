using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using poke_fall_api.Models;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorMoves : Migration
    {
        readonly string[] TUTOR_MOVE_COLUMNS = new string[]
        {
            "Id",
            "PokemonId",
            "MoveId",
            "VersionId"
        };
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false),
                    VersionId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorMoves", x => x.Id);
                });

            seedTutorMoves(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorMoves");
        }

        private void seedTutorMoves(MigrationBuilder migrationbuilder)
        {
            var insertMoves = new List<TutorMove>();
            var moveId = 1;
            using (var moveReader = new StreamReader(@"./data/pokemon_moves.csv"))
            using (var moveCsv = new CsvReader(moveReader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var moves = moveCsv.GetRecords<PokeApiPokemonMove>().ToArray();
                foreach (PokeApiPokemonMove move in moves)
                {
                    if (move.pokemon_move_method_id == 3)
                    {
                    migrationbuilder.InsertData(
                        table: "TutorMoves",
                        columns: TUTOR_MOVE_COLUMNS,
                        values: new object[]
                        {
                            moveId,
                            move.pokemon_id,
                            move.move_id,
                            move.version_group_id
                        }
                    );
                    moveId++;
                    }
                }
            }
        }


        private class PokeApiPokemonMove
        {
            public int pokemon_id { get; set; }
            public int version_group_id { get; set; }
            public int move_id { get; set; }
            public int pokemon_move_method_id { get; set; }
            public int level { get; set; }
        }
    }
}
