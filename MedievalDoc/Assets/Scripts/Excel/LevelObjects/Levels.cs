using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Data {
    public class Levels
    {
        [JsonProperty] public string levelID { get; set; }
        [JsonProperty] public string levelOrder { get; set; }
        [JsonProperty] public string levelPrefab { get; set; }
        [JsonProperty] public string sicknessContainer { get; set; }
        [JsonProperty] public string starsRange { get; set; }
        [JsonProperty] public string toolNotes { get; set; }

        public string[] starsRangeList;

        [OnDeserialized]
        public void DeserializeArrays(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(starsRange, ref starsRangeList);
        }
    }
}
