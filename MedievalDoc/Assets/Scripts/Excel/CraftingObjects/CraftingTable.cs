using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Data
{
    public class CraftingTable
    {
        [JsonProperty] public string craftingID { get; set; }
        [JsonProperty] public string recipesContainer { get; set; }
        [JsonProperty] public string interactionTime { get; set; }

        public string[] recipes;

        [OnDeserialized]
        public void DeserializeArrays(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(recipesContainer, ref recipes);
        }
    }
}