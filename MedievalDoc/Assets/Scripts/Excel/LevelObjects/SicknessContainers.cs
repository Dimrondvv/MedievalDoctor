using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Data
{
    public class SicknessContainers
    {
        [JsonProperty] public string key { get; set; }
        [JsonProperty] public string sicknessCount { get; set; }
        [JsonProperty] public string sicknessArray { get; set; }
        [JsonProperty] public string toolNotes { get; set; }

        public string[] levelSicknesses;

        [OnDeserialized]
        public void DeserializeArrays(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(sicknessArray, ref levelSicknesses);
        }
    }
}
