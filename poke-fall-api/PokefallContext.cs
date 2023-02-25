using Microsoft.EntityFrameworkCore;

namespace poke_fall_api.Models
{
    public class PokefallContext : DbContext
    {
        public PokefallContext(DbContextOptions<PokefallContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Ability> Abilities { get; set; }
    }
}