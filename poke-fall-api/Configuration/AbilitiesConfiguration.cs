using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poke_fall_api.Models;
using System.IO;

namespace poke_fall_api.Configuration
{
    public class AbilitiesConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.ToTable("Abilities");
            builder.Property(a => a.Id).IsRequired(true);
            builder.Property(a => a.Name).IsRequired(true);
            builder.Property(a => a.Description).IsRequired(true);

            Ability[] abilities = new Ability[]{};

            foreach (string line in File.ReadLines(@"/Users/akmindt/Projects/pokeapi/data/v2/csv/abilities.csv"))
            {
                string[] values = line.Split(",");
                foreach (string descLines in File.ReadLines(@"/Users/akmindt/Projects/pokeapi/data/v2/csv/ability_flavor_text.csv"))
                {
                    string[] descValues = line.Split(",");
                    if (descLines[1].Equals("10") && values[0].Equals(descValues[0]))
                    {
                        abilities.Append(
                            new Ability
                            {
                                Id = Int32.Parse(values[0]),
                                Name = values[1],
                                Description = descValues[2]
                            }
                        );
                    }
                }
            }
            builder.HasData(abilities);
        }
    }
}