using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data.Description
{
    public class Items
    {
        [JsonProperty] public string itemDescription { get; set; }
        [JsonProperty] public string description { get; set; }
    }
}
