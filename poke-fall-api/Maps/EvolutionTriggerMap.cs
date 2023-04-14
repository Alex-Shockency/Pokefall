public class EvolutionTriggerMap
{
    private Dictionary<Int32, string> evoTriggerMap = new Dictionary<int, string>();

    public EvolutionTriggerMap()
    {
        this.evoTriggerMap.Add(0, null);
        this.evoTriggerMap.Add(1, "Level Up");
        this.evoTriggerMap.Add(2, "Trade");
        this.evoTriggerMap.Add(3, "Use Item");
        this.evoTriggerMap.Add(4, "Shed");
        this.evoTriggerMap.Add(5, "Spin");
        this.evoTriggerMap.Add(6, "Tower of Darkness");
        this.evoTriggerMap.Add(7, "Tower of Waters");
        this.evoTriggerMap.Add(8, "Three Critical Hits");
        this.evoTriggerMap.Add(9, "Take Damage");
        this.evoTriggerMap.Add(10, "Other");
        this.evoTriggerMap.Add(11, "Agile Style Move");
        this.evoTriggerMap.Add(12, "Strong Style Move");
        this.evoTriggerMap.Add(13, "Recoil Damage");
    }

    public Dictionary<Int32, string> getEvoTriggerMap()
    {
        return this.evoTriggerMap;
    }
}
