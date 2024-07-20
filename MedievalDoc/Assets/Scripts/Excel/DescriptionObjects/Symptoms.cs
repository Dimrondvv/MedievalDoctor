using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data.Description
{
    public class Symptoms
    {
        [JsonProperty] public string symptomsDescription { get; set; }
        [JsonProperty] public string description { get; set; }
    }
}
