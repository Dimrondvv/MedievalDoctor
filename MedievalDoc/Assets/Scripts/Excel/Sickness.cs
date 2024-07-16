using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data
{
    public class Sickness
    {
        [JsonProperty] public string sicknessID { get; set; }
        [JsonProperty] public string sicknessName { get; set; }
        [JsonProperty] public string sicknessDescription { get; set; }
        [JsonProperty] public string sicknessStory { get; set; }
        [JsonProperty] public string symptonsContainer { get; set; }
    }
}
