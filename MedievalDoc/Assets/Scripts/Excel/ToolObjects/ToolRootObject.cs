using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data {
    public class ToolRootObject
    {
        [JsonProperty] public Tool[] tools { get; set; }
        [JsonProperty] public ToolChest[] toolChest { get; set; }
    }
}
