using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data {
    public class SicknessRootObject
    {
        [JsonProperty] public Sickness[] sickness { get; set; }
        [JsonProperty] public Symptom[] symptoms { get; set; }
        [JsonProperty] public SymptomDependencies[] dependencies { get; set; }
    }
}
