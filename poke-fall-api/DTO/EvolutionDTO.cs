using Microsoft.EntityFrameworkCore;
using poke_fall_api.Models;

namespace poke_fall_api.DTO
{
    [Keyless]
    public class EvolutionDTO
    {
        public int Id { get; set; }
        public int PokedexNumber { get; set; }
       public int EvolvedFromPokedexNumber { get; set; }
        public string EvolutionTrigger { get; set; }
        public string TriggerItem { get; set; }
        public int MinLevel { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string HeldItem { get; set; }
        public string TimeOfDay { get; set; }
        public int KnownMoveId { get; set; }
        public string KnownMoveType { get; set; }
        public int MinHappiness { get; set; }
        public int MinBeauty { get; set; }
        public int MinAffection { get; set; }
        public string RelativePhysicalStats { get; set; }
        public int PartyPokedexNumber { get; set; }
        public string PartyType { get; set; }
        public int TradePokedexNumber { get; set; }
        public bool NeedsOverworldRain { get; set; }
        public bool TurnUpsideDown { get; set; }
    }
}
