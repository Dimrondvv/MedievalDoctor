using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Data
{
    public class Recipes
    {
        [JsonProperty] public string recipeID { get; set; }
        [JsonProperty] public string recipeName { get; set; }
        [JsonProperty] public string recipeDescription { get; set; }
        [JsonProperty] public string itemContainer { get; set; }
        [JsonProperty] public string recipeResult { get; set; }
        [JsonProperty] public string recipeTime { get; set; }
        [JsonProperty] public string itemsDestroyed { get; set; }

        public string[] itemsRequired;
        public string[] destroyedOnCraft;

        [OnDeserialized]
        public void DeserializeArrays(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(itemContainer, ref itemsRequired);
            ImportJsonData.ConvertJsonToArray(itemsDestroyed, ref destroyedOnCraft);
        }
    }
}