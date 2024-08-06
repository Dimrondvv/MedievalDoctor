using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Data.Description
{
    public class Recipes
    {
        [JsonProperty] public string recipeDescription { get; set; }
        [JsonProperty] public string description { get; set; }
    }
}
