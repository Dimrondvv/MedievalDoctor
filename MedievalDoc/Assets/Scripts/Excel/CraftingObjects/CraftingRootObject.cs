using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data
{
    public class CraftingRootObject
    {
        [JsonProperty] public CraftingTable[] crafting_Tables;
        [JsonProperty] public Recipes[] recipes;
        [JsonProperty] public ItemChest[] itemChest;
        [JsonProperty] public Items[] items;
        [JsonProperty] public ItemChanger[] itemChanger;
    }
}
