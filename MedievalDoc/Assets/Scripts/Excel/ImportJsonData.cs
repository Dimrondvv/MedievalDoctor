using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Data
{
    public class ImportJsonData : MonoBehaviour
    {
        #region sickness
        public static Sickness[] sicknessConfig;
        public static Symptom[] symptomConfig;
        public static SymptomDependencies[] symptomDependenciesConfig;
        #endregion
        #region level
        public static Levels[] levelConfig;
        public static SicknessContainers[] sicknessContainersConfig;
        #endregion
        #region tools
        public static Tool[] toolConfig;
        public static ToolChest[] toolChestConfig;
        #endregion
        #region craftings
        public static CraftingTable[] craftingTables;
        public static Recipes[] recipes;
        public static ItemChest[] itemChests;
        public static ItemChanger[] itemChangers;
        public static Items[] items;
        #endregion
        #region descriptions
        public static Description.Recipes[] recipeDescriptions;
        public static Description.Items[] itemDescriptions;
        public static Description.Sicknesses[] sicknessDescriptions;
        public static Description.Symptoms[] symptomDescriptions;
        public static Description.Tools[] toolDescriptions;
        #endregion
        void Start()
        {
            DeserializeSicknessConfig();
            DeserializeLevelConfig();
            DeserializeToolConfig();
            //DeserializeCraftingConfig();
            DeserializeDescriptionConfig();
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
        private void DeserializeLevelConfig()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Configs/level_config.json");
            LevelRootObject levelRoot = JsonConvert.DeserializeObject<LevelRootObject>(json);
            levelConfig = levelRoot.levels;
            sicknessContainersConfig = levelRoot.sicknessContainers;
        }
        private void DeserializeToolConfig()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Configs/tool_config.json");
            ToolRootObject toolRoot = JsonConvert.DeserializeObject<ToolRootObject>(json);
            toolConfig = toolRoot.tools;
            toolChestConfig = toolRoot.toolChest;
        }
        private void DeserializeCraftingConfig()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Configs/craft_config.json");
            CraftingRootObject craftRoot = JsonConvert.DeserializeObject<CraftingRootObject>(json);
            craftingTables = craftRoot.craftingTables;
            recipes = craftRoot.recipes;
            itemChests = craftRoot.itemChest;
            itemChangers = craftRoot.itemChanger;
            items = craftRoot.items;
        }
        private void DeserializeDescriptionConfig()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Configs/description_config.json");
            DescrpitionRootObject descriptionRoot = JsonConvert.DeserializeObject<DescrpitionRootObject>(json);
            recipeDescriptions = descriptionRoot.recipes;
            itemDescriptions = descriptionRoot.items;
            sicknessDescriptions = descriptionRoot.sicknesses;
            symptomDescriptions = descriptionRoot.symptoms;
            toolDescriptions = descriptionRoot.tools;

            Debug.Log(toolDescriptions);
        }
    }
}
