using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data.Description
{
    public class Sicknesses
    {
        [JsonProperty] public string sicknessDescription { get; set; }
        [JsonProperty] public string description { get; set; }
    }
}
