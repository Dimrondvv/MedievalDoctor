using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

[System.Serializable]
public class DiscoveredData
{
    [JsonProperty] public string name { get; set; }
    [JsonProperty] public string description { get; set; }
    [JsonIgnore] public Sprite icon { get; set; }
    [JsonProperty] public string iconName { get; set; }

    public DiscoveredData(string name, string description, string iconName)
    {
        this.name = name;
        this.description = description;
        this.iconName = iconName;
    }

    

    [OnDeserialized]
    public void DeserializeIcon(StreamingContext context)
    {
        icon = Resources.Load<Sprite>($"Icons/{iconName}");
    }
}
