using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data
{
    public class ItemChanger
    {
        [JsonProperty] public string changerID { get; set; }
        [JsonProperty] public string itemReq { get; set; }
        [JsonProperty] public string itemResult { get; set; }

    }
}