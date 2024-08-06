using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data
{
    public class Items
    {
        [JsonProperty] public string itemID { get; set; }
        [JsonProperty] public string itemDescription { get; set; }

    }
}