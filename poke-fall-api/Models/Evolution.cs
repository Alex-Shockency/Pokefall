using System.ComponentModel.DataAnnotations;

namespace poke_fall_api.Models
{
    public class Evolution
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EvolvedPokedexNumber { get; set; }
        [Required]
        public int EvolvedFromPokedexNumber { get; set; }
        [Required]
        public string EvolutionTrigger { get; set; }
        public string TriggerItem { get; set; }
        public int MinLevel { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string HeldItem { get; set; }
        public string TimeOfDay { get; set; }
         public int KnownMoveId { get; set; }
        public string KnownMoveType { get; set; }
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
