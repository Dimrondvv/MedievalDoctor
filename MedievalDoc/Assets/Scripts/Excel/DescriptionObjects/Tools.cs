using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data.Description 
{ 
    public class Tools 
    {
        [JsonProperty] public string toolDescription { get; set; }
        [JsonProperty] public string description { get; set; }
    }
}
