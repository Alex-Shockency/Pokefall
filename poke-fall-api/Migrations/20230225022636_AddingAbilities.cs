using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            string[] columns = new string[]{"Id", "Name", "Description"};

            foreach (string line in File.ReadLines(@"/Users/akmindt/Projects/pokeapi/data/v2/csv/abilities.csv"))
                {
                    string[] values = line.Split(",");
                        foreach (string descLine in File.ReadLines(@"/Users/akmindt/Projects/pokeapi/data/v2/csv/ability_flavor_text.csv"))
                        {
                            string[] descValues = descLine.Split(",");
                            if (descValues[2].Equals("9") && Int32.Parse(descValues[1]) >= 10 && values[0].Equals(descValues[0]))
                            {
                                string[] dbValues = new string[]{values[0], values[1], descValues[3]};
                                migrationBuilder.InsertData("Abilities", columns, dbValues);
                            }
                        }
                    }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abilities");
        }
    }
}
