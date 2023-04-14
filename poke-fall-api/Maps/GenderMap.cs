public class GenderMap
{
    private Dictionary<Int32, string> genderMap = new Dictionary<int, string>();

    public GenderMap()
    {
        this.genderMap.Add(0, null);
        this.genderMap.Add(1, "female");
        this.genderMap.Add(2, "male");
        this.genderMap.Add(3, "genderless");
    }
    public Dictionary<Int32, string> getGenderMap()
    {
        return this.genderMap;
    }
}
