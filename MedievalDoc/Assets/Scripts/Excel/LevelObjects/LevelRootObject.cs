using Newtonsoft.Json;
namespace Data
{
    public class LevelRootObject
    {
        [JsonProperty] public Levels[] levels { get; set; }
        [JsonProperty] public SicknessContainers[] sicknessContainers { get; set; }
    }
}
