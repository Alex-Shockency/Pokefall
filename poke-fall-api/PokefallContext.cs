using Microsoft.EntityFrameworkCore;
using poke_fall_api.Models;

namespace poke_fall_api.Context
{
    public class PokefallContext : DbContext
    {
        public PokefallContext(DbContextOptions<PokefallContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}