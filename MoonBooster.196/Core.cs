using UnityEngine;

namespace MoonBooster
{
    public class Core
    {
        public static void Start()
        {
            Object.DontDestroyOnLoad(new GameObject(null, typeof(Menu)) { hideFlags = HideFlags.HideAndDontSave });
        }
    }
}
