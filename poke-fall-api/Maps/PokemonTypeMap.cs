public class PokemonTypeMap
{
    private Dictionary<Int32, string> typeMap = new Dictionary<int, string>();

    public PokemonTypeMap()
    {
        this.typeMap.Add(1, "normal");
        this.typeMap.Add(2, "fighting");
        this.typeMap.Add(3, "flying");
        this.typeMap.Add(4, "poison");
        this.typeMap.Add(5, "ground");
        this.typeMap.Add(6, "rock");
        this.typeMap.Add(7, "bug");
        this.typeMap.Add(8, "ghost");
        this.typeMap.Add(9, "steel");
        this.typeMap.Add(10, "fire");
        this.typeMap.Add(11, "water");
        this.typeMap.Add(12, "grass");
        this.typeMap.Add(13, "electric");
        this.typeMap.Add(14, "psychic");
        this.typeMap.Add(15, "ice");
        this.typeMap.Add(16, "dragon");
        this.typeMap.Add(17, "dark");
        this.typeMap.Add(18, "fairy");
    }

    public  Dictionary<Int32, string> getTypeMap(){
        return this.typeMap;
    }
}
