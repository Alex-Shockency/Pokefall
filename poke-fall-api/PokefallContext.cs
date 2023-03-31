using Microsoft.EntityFrameworkCore;

namespace poke_fall_api.Models
{
    public class PokefallContext : DbContext
    {
        public PokefallContext(DbContextOptions<PokefallContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<LevelUpMove> LevelUpMoves { get; set; } 
        public DbSet<EggMove> EggMoves { get; set; }
        public DbSet<TutorMove> TutorMoves { get; set; }
        public DbSet<TMMove> TMMoves { get; set; }
    }
}