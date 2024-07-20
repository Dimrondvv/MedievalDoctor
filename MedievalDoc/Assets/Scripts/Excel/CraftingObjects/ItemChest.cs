using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data
{
    public class ItemChest
    {
        [JsonProperty] public string chestID { get; set; }
        [JsonProperty] public string itemID { get; set; }

    }
}