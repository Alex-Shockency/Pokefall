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
