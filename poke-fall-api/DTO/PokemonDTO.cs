namespace poke_fall_api.DTO
{
    public class PokemonDTO
    {
        public int PokemonId { get; set; }
        public int PokedexNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Ability1Id { get; set; }
        public int Ability2Id { get; set; }
        public int HiddenAbilityId { get; set; }
        public int CatchRate { get; set; }
        public List<(string,  int)> GenderRatio { get; set; }
        public string EggGroup { get; set; }
        public int HatchStepsMin { get; set; }
        public int HatchStepMax { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseEXPYield { get; set; }
        public string LevelingRate { get; set; }
        public Array EVYield { get; set; }
        public int BaseFriendship { get; set; }
        public Array BaseStats { get; set; }
        public List<(string, int)> LevelUpMoves { get; set; }
        public Array TMMoves { get; set; }
        public Array EggMoves { get; set; }
        public Array TutorMoves { get; set; }
        public int EvolvesTo { get; set; }
        public int EvolvesFrom { get; set; }
        public string? EvolutionDescription { get; set; }
    }
}