using Smaa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoonBooster
{
    public class RuntimeMethods : MonoBehaviour
    {
        #region FoliageCell
        [Obfuscation(Exclude = true, Feature = "")]
        private static float CalculateLOD()
        {
            return 1f;
        }

        #endregion

        #region SunLight
        [Obfuscation(Exclude = true, Feature = "")]
        private void Update()
        {
            if (light == null)
                this.light = base.GetComponent<Light>();
            LightShadows lightShadows = 0;
            if (this.light.shadows != lightShadows)
            {
                this.light.shadows = lightShadows;
            }
        }
        [Obfuscation(Exclude = true, Feature = "")]
        private Light light;

        #endregion

        #region SMAA
        [Obfuscation(Exclude = true, Feature = "")]
        public void SMMAInit()
        {
            preset = new Preset
            {
                Threshold = 0.45f,
                MaxSearchSteps = 2
            };
            preset.DiagDetection = false;
            preset.CornerDetection = false;
            preset.CornerRounding = 10;
            this.AreaTex = Resources.Load<Texture2D>("AreaTex");
            this.SearchTex = Resources.Load<Texture2D>("SearchTex");
            this.m_Camera = base.GetComponent<Camera>();
        }
        [Obfuscation(Exclude = true, Feature = "")]
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (m_Camera == null)
                SMMAInit();
            int pixelWidth = this.m_Camera.pixelWidth;
            int pixelHeight = this.m_Camera.pixelHeight;
            int detectionMethod = (int)this.DetectionMethod;
            int pass = 4;
            int pass2 = 5;
            this.Material.SetTexture("_AreaTex", this.AreaTex);
            this.Material.SetTexture("_SearchTex", this.SearchTex);
            this.Material.SetTexture("_SourceTex", source);
            this.Material.SetVector("_Metrics", new Vector4(0.8f / (float)pixelWidth, 0.8f / (float)pixelHeight, (float)pixelWidth, (float)pixelHeight));
            this.Material.SetVector("_Params1", new Vector4(preset.Threshold, preset.DepthThreshold, (float)preset.MaxSearchSteps, (float)preset.MaxSearchStepsDiag));
            this.Material.SetVector("_Params2", new Vector2((float)preset.CornerRounding, preset.LocalContrastAdaptationFactor));
            Shader.DisableKeyword("USE_PREDICATION");
            if (this.DetectionMethod == EdgeDetectionMethod.Depth)
            {
                this.m_Camera.depthTextureMode |= DepthTextureMode.Depth;
            }
            else if (this.UsePredication)
            {
                this.m_Camera.depthTextureMode |= DepthTextureMode.Depth;
                Shader.EnableKeyword("USE_PREDICATION");
                this.Material.SetVector("_Params3", new Vector3(this.CustomPredicationPreset.Threshold, this.CustomPredicationPreset.Scale, this.CustomPredicationPreset.Strength));
            }
            Shader.DisableKeyword("USE_DIAG_SEARCH");
            Shader.DisableKeyword("USE_CORNER_DETECTION");
            if (preset.DiagDetection)
            {
                Shader.EnableKeyword("USE_DIAG_SEARCH");
            }
            if (preset.CornerDetection)
            {
                Shader.EnableKeyword("USE_CORNER_DETECTION");
            }
            RenderTexture renderTexture = this.TempRT(pixelWidth, pixelHeight);
            RenderTexture renderTexture2 = this.TempRT(pixelWidth, pixelHeight);
            this.Clear(renderTexture);
            this.Clear(renderTexture2);
            Graphics.Blit(source, renderTexture, this.Material, detectionMethod);
            if (this.DebugPass == DebugPass.Edges)
            {
                Graphics.Blit(renderTexture, destination);
            }
            else
            {
                Graphics.Blit(renderTexture, renderTexture2, this.Material, pass);
                if (this.DebugPass == DebugPass.Weights)
                {
                    Graphics.Blit(renderTexture2, destination);
                }
                else
                {
                    Graphics.Blit(renderTexture2, destination, this.Material, pass2);
                }
            }
            RenderTexture.ReleaseTemporary(renderTexture);
            RenderTexture.ReleaseTemporary(renderTexture2);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        public Material Material
        {
            get
            {
                if (this.m_Material == null)
                {
                    this.m_Material = new Material(this.Shader);
                    this.m_Material.hideFlags = HideFlags.HideAndDontSave;
                }
                return this.m_Material;
            }
        }
        [Obfuscation(Exclude = true, Feature = "")]
        private RenderTexture TempRT(int width, int height)
        {
            int depthBuffer = 0;
            return RenderTexture.GetTemporary(width, height, depthBuffer, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        private void Clear(RenderTexture rt)
        {
            Graphics.Blit(rt, rt, this.Material, 0);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        Preset preset;
        [Obfuscation(Exclude = true, Feature = "")]
        public PredicationPreset CustomPredicationPreset;
        [Obfuscation(Exclude = true, Feature = "")]
        public EdgeDetectionMethod DetectionMethod = EdgeDetectionMethod.Luma;
        [Obfuscation(Exclude = true, Feature = "")]
        public bool UsePredication;
        [Obfuscation(Exclude = true, Feature = "")]
        public Texture2D SearchTex;
        [Obfuscation(Exclude = true, Feature = "")]
        public Texture2D AreaTex;
        [Obfuscation(Exclude = true, Feature = "")]
        protected Camera m_Camera = null;
        [Obfuscation(Exclude = true, Feature = "")]
        public QualityPreset Quality = QualityPreset.High;
        [Obfuscation(Exclude = true, Feature = "")]
        public DebugPass DebugPass;
        [Obfuscation(Exclude = true, Feature = "")]
        protected Material m_Material;
        [Obfuscation(Exclude = true, Feature = "")]
        public Shader Shader;

        #endregion


        #region  LODUtil
        [Obfuscation(Exclude = true, Feature = "")]
        public static float VerifyDistance250(float distance)
        {
            return Mathf.Min(250f, distance);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        public static float VerifyDistance300(float distance)
        {
            return Mathf.Min(300f, distance);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        public static float VerifyDistance350(float distance)
        {
            return Mathf.Min(350f, distance);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        public static float VerifyDistance400(float distance)
        {
            return Mathf.Min(400f, distance);
        }
        [Obfuscation(Exclude = true, Feature = "")]
        public static float VerifyDistance450(float distance)
        {
            return Mathf.Min(450f, distance);
        }


        #endregion
    }
}
