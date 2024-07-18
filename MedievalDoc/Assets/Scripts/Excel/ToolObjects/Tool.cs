using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Data
{
    public class Tool
    {
        [JsonProperty] public string toolID { get; set; }
        [JsonProperty] public string toolPrefab { get; set; }
        [JsonProperty] public string toolName { get; set; }
        [JsonProperty] public string toolDescription { get; set; }
        [JsonProperty] public string toolIcon { get; set; }
        [JsonProperty] public string symptomAdd { get; set; }
        [JsonProperty] public string symptomRemove { get; set; }
        [JsonProperty] public string interactionTime { get; set; }
        [JsonProperty] public string oneUse { get; set; }

        public string[] symptomsAdded;
        public string[] symptomsRemoved;

        [OnDeserialized]
        public void DeserializeContainer(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(symptomAdd, ref symptomsAdded);
            ImportJsonData.ConvertJsonToArray(symptomRemove, ref symptomsRemoved);
        }

    }
}
