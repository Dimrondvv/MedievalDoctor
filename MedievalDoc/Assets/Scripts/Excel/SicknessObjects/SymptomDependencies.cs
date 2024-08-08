using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Data
{
    public class SymptomDependencies
    {
        [JsonProperty] public string symptomID { get; set; }
        [JsonProperty] public string symptomIDreqtoremove { get; set; }
        [JsonProperty] public string symptomIDreqtoadd { get; set; }
        [JsonProperty] public string symptomIDblockremove { get; set; }
        [JsonProperty] public string symptomIDblockadd { get; set; }
        [JsonProperty] public string symptomIDaddonremove { get; set; }

        public string[] symptomsRequiredToRemove;
        public string[] symptomsRequiredToAdd;
        public string[] symptomsBlockingRemove;
        public string[] symptomsBlockingAdd;
        public string[] symptomsAddOnRemove;

        [OnDeserialized]
        public void DeserializeContainer(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(symptomIDreqtoremove, ref symptomsRequiredToRemove);
            ImportJsonData.ConvertJsonToArray(symptomIDreqtoadd, ref symptomsRequiredToAdd);
            ImportJsonData.ConvertJsonToArray(symptomIDblockremove, ref symptomsBlockingRemove);
            ImportJsonData.ConvertJsonToArray(symptomIDblockadd, ref symptomsBlockingAdd);
            ImportJsonData.ConvertJsonToArray(symptomIDaddonremove, ref symptomsAddOnRemove);
        }
    }
}
