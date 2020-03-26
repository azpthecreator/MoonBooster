using System;
using System.IO;
using Newtonsoft.Json;

namespace MoonBooster
{
    class Config
    {
        public static Setting _setting = new Setting();

        public class Setting
        {
            [JsonProperty("DisableShadows")]
            public bool DisableShadows = false;

            [JsonProperty("DisableGrass")]
            public bool DisableGrass = false;

            [JsonProperty("DisableAdditionals")]
            public bool DisableAdditionals = false;

            [JsonProperty("RenderDistance")]
            public int RenderDistance = 500;

            [JsonProperty("currentId")]
            public int currentId = 2;
        }

        static string path = Environment.CurrentDirectory + "\\MoonBooster.json";

        public static void LoadSettings()
        {
            if (File.Exists(path))
            {
                _setting = JsonConvert.DeserializeObject<Setting>(File.ReadAllText(path));
                ManageSettings.ChangeDistance();
                ManageSettings.ChangePreset();
                ManageSettings.ChangeShadows();
                ManageSettings.ChangeVisibleGrass();
            }
        }

        public static void SaveSettings()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(_setting, Formatting.Indented));
        }
    }
}
