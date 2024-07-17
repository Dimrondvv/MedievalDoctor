using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Data
{
    public class ImportJsonData : MonoBehaviour
    {

        public static Sickness[] sicknessConfig;
        public static Symptom[] symptomConfig;
        public static SymptomDependencies[] symptomDependenciesConfig;

        void Start()
        {
            DeserializeSicknessConfig();
        }

        //Helper function to convert json string seperated with comas into arrays
        public static void ConvertJsonToArray(string jsonStream, ref string[] array)
        {
            if (jsonStream == null)
                return;
            array = jsonStream.Split(',');
        }

        private void DeserializeSicknessConfig()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Configs/sickness_config.json");
            SicknessRootObject sicknessRoot = JsonConvert.DeserializeObject<SicknessRootObject>(json);
            sicknessConfig = sicknessRoot.sickness;
            symptomConfig = sicknessRoot.symptoms;
            symptomDependenciesConfig = sicknessRoot.dependencies;
        }
    }
}
