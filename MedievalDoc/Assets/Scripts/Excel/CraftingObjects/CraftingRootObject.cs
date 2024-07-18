using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Data
{
    public class CraftingRootObject
    {
        [JsonProperty] public CraftingTable[] craftingTables;
        [JsonProperty] public Recipes[] recipes;
        [JsonProperty] public ItemChest[] itemChest;
        [JsonProperty] public Items[] items;
        [JsonProperty] public ItemChanger[] itemChanger;
    }
}
