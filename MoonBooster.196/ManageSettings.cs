using Smaa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoonBooster
{
    class ManageSettings
    {
        static HookManager hook = new HookManager();

        public static void ChangeVisibleGrass()
        {
            MethodInfo origMeth = typeof(FoliageCell).GetMethod("CalculateLOD", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);
            if (Config._setting.DisableGrass)
            {
                MethodInfo replMeth = typeof(RuntimeMethods).GetMethod("CalculateLOD", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                hook.Hook(origMeth, replMeth);
            }
            else
                if (hook.HookExists(origMeth))
                    hook.Unhook(origMeth);
        }

        public static void ChangeShadows()
        {
            MethodInfo origMeth = typeof(SunSettings).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);
            if (Config._setting.DisableShadows)
            {
                MethodInfo replMeth = typeof(RuntimeMethods).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);
                hook.Hook(origMeth, replMeth);
            }
            else
                if (hook.HookExists(origMeth))
                    hook.Unhook(origMeth);
        }

        public static void ChangePreset()
        {
            MethodInfo origMeth = typeof(SMAA).GetMethod("OnRenderImage", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);
            if (Config._setting.DisableAdditionals)
            {
                MethodInfo replMeth = typeof(RuntimeMethods).GetMethod("OnRenderImage", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);
                hook.Hook(origMeth, replMeth);
            }
            else
                if (hook.HookExists(origMeth))
                    hook.Unhook(origMeth);
        }

        public static void ChangeDistance()
        {
            MethodInfo origMeth = typeof(LODUtil).GetMethod("VerifyDistance", System.Reflection.BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (hook.HookExists(origMeth))
                hook.Unhook(origMeth);

            if (Config._setting.RenderDistance == 500)
                return;

            MethodInfo replMeth = typeof(RuntimeMethods).GetMethod("VerifyDistance" + Config._setting.RenderDistance, System.Reflection.BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            hook.Hook(origMeth, replMeth);
        }
    }
}
