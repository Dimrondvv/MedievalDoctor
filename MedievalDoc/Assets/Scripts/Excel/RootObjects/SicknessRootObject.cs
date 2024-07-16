using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data {
    public class SicknessRootObject
    {
        [JsonProperty] public Sickness[] sicknesses { get; set; }
        [JsonProperty] public Symptom[] symptoms { get; set; }
        [JsonProperty] public SymptomDependencies[] symptomDependencies { get; set; }
    }
}
