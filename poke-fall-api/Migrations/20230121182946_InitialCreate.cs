using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pokefallapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokedexNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type1 = table.Column<int>(type: "integer", nullable: false),
                    Type2 = table.Column<int>(type: "integer", nullable: false),
                    Ability1Id = table.Column<int>(type: "integer", nullable: false),
                    Ability2Id = table.Column<int>(type: "integer", nullable: false),
                    HiddenAbilityId = table.Column<int>(type: "integer", nullable: false),
                    CatchRate = table.Column<int>(type: "integer", nullable: false),
                    EggGroup = table.Column<int>(type: "integer", nullable: false),
                    HatchStepsMin = table.Column<int>(type: "integer", nullable: false),
                    HatchStepMax = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    BaseEXPYield = table.Column<int>(type: "integer", nullable: false),
                    LevelingRate = table.Column<string>(type: "text", nullable: false),
                    BaseFriendship = table.Column<int>(type: "integer", nullable: false),
                    EvolvesTo = table.Column<int>(type: "integer", nullable: false),
                    EvolvesFrom = table.Column<int>(type: "integer", nullable: false),
                    EvolutionDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonId);
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    PP = table.Column<int>(type: "integer", nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: false),
                    Accuracy = table.Column<int>(type: "integer", nullable: false),
                    Contact = table.Column<bool>(type: "boolean", nullable: false),
                    AffectedProtect = table.Column<bool>(type: "boolean", nullable: false),
                    AffectedMagicCoat = table.Column<bool>(type: "boolean", nullable: false),
                    AffectedSnatch = table.Column<bool>(type: "boolean", nullable: false),
                    AffectedMirrorMove = table.Column<bool>(type: "boolean", nullable: false),
                    AffectedKingsRock = table.Column<bool>(type: "boolean", nullable: false),
                    PokemonId = table.Column<int>(type: "integer", nullable: true),
                    PokemonId1 = table.Column<int>(type: "integer", nullable: true),
                    PokemonId2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId");
                    table.ForeignKey(
                        name: "FK_Move_Pokemons_PokemonId1",
                        column: x => x.PokemonId1,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId");
                    table.ForeignKey(
                        name: "FK_Move_Pokemons_PokemonId2",
                        column: x => x.PokemonId2,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Move_PokemonId",
                table: "Move",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PokemonId1",
                table: "Move",
                column: "PokemonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Move_PokemonId2",
                table: "Move",
                column: "PokemonId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
