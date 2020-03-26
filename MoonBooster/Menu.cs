using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MoonBooster
{
    public class Menu : MonoBehaviour
    {
        GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle;
        float delay = 0, widthSize = 300;

        bool isOpen = false;
        Rect rect = new Rect(40f, 40f, 100f, 100f);

        Texture2D ontexture, onpresstexture, offtexture, offpresstexture, backtexture, btntexture, btnpresstexture;
        Texture2D NewTexture2D { get { return new Texture2D(1, 1); } }

        int btnY, mulY;
        [Obfuscation(Exclude = true, Feature = "")]
        void Update()
        {
            //AppDomain.CurrentDomain.GetAssemblies()
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                isOpen = !isOpen;
                Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = isOpen;
                if (!isOpen)
                {
                    ManageSettings.ChangeShadows();
                    ManageSettings.ChangeVisibleGrass();
                    ManageSettings.ChangePreset();
                    ManageSettings.ChangeDistance();
                    Config.SaveSettings();
                }
            }
        }
        [Obfuscation(Exclude = true, Feature = "")]
        void OnGUI()
        {
            if (isOpen)
                ModMenuGUI();
        }

        float GetHeight(int i = 1) { return 24 * i; }

        void ModMenuGUI()
        {
            GUI.skin = skin;
            GUI.Window(0, new Rect(150, 150, 210, 200), new GUI.WindowFunction((id) =>
            {
                Config._setting.DisableShadows = GUI.Toggle(new Rect(28, 28, 150, 20), Config._setting.DisableShadows, "Disable Shadows");
                Config._setting.DisableGrass = GUI.Toggle(new Rect(28, 28 + GetHeight(), 150, 20), Config._setting.DisableGrass, "Disable Grass");
                Config._setting.DisableAdditionals = GUI.Toggle(new Rect(28, 28 + GetHeight(2), 150, 20), Config._setting.DisableAdditionals, "Disable Add's");
                GUI.Label(new Rect(28, 28 + GetHeight(3), 150, 20), $"RDistance: {GetDist()}");
                Config._setting.currentId = (int)GUI.HorizontalSlider(new Rect(28, 28 + GetHeight(4), 150, 20), Config._setting.currentId, 0, 3);
            }), "");
        }

        int GetDist()
        {
            switch (Config._setting.currentId)
            {
                case 0:
                    return 350;
                case 1:
                    return 400;
                case 2:
                    return 450;
                case 3:
                    return 500;
            }
            return 0;
        }

        void ChangeDist()
        {
            switch (Config._setting.currentId)
            {
                case 0:
                    Config._setting.RenderDistance = 350;
                    break;
                case 1:
                    Config._setting.RenderDistance = 400;
                    break;
                case 2:
                    Config._setting.RenderDistance = 450;
                    break;
                case 3:
                    Config._setting.RenderDistance = 500;
                    break;
            }
        }

        GUISkin skin;

        [Obfuscation(Exclude = true, Feature = "")]
        void Start()
        {
            skin = AssetBundle.LoadFromMemory(Data.skin).LoadAsset<GUISkin>("skin");
            Config.LoadSettings();
        }
    }
}
