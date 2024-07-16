using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data
{
    public class SymptomDependencies
    {
        [JsonProperty] public string? symptomID { get; set; }
        [JsonProperty] public string? symptomIDreqtoremove { get; set; }
        [JsonProperty] public string? symptomIDreqtoadd { get; set; }
        [JsonProperty] public string? symptomIDblockremove { get; set; }
        [JsonProperty] public string? symptomIDblockadd { get; set; }
        [JsonProperty] public string? symtpomIDaddonremove { get; set; }
    }
}
