using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Data
{
    public class Symptom
    {
        [JsonProperty] public string symptomID { get; set; }
        [JsonProperty] public string symptomName { get; set; }
        [JsonProperty] public string symptomDescription { get; set; }
        [JsonProperty] public string symptomIcon { get; set; }
        [JsonProperty] public string symptomLocalization { get; set; }
        [JsonProperty] public string symptomDmg { get; set; }
        [JsonProperty] public string symptomPoints { get; set; }
        [JsonProperty] public string partOverride { get; set; }

        public string[] symptomLocalizationArray;

        [OnDeserialized]
        public void DeserializeContainer(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(symptomLocalization, ref symptomLocalizationArray);
        }
    }
}
