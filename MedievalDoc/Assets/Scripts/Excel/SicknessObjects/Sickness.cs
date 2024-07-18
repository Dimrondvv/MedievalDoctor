using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Data
{
    public class Sickness
    {
        [JsonProperty] public string sicknessID { get; set; }
        [JsonProperty] public string sicknessName { get; set; }
        [JsonProperty] public string sicknessDescription { get; set; }
        [JsonProperty] public string sicknessStory { get; set; }
        [JsonProperty] public string symptomsContainer { get; set; }
        public string[] symptomsContainerList;

        [OnDeserialized]
        public void DeserializeContainer(StreamingContext context)
        {
            ImportJsonData.ConvertJsonToArray(symptomsContainer, ref symptomsContainerList);
        }

    }
}
