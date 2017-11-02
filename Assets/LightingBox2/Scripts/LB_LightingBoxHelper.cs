// Use this script to get runtime access to the lighting box econtrolled effects
/// <summary>
/// example :
/// 
/// // Update bloom effect .
/// void Start ()
/// {
///   	GameObject.FindObjectOfType<LB_LightingBoxHelper> ().Update_Bloom (true, 1f, 0.5f, Color.white);
/// }
/// </summary>
using UnityEngine;   
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using LightingBox.Effects;

#if UNITY_EDITOR
using UnityEditor;
#endif

#region Emum Types

public enum CameraMode
{
	Single,All,Custom
}
public enum WindowMode
{
	Part1,Part2,Part3,
	Finish
}
public enum AmbientLight
{
	Skybox,
	Color
}
public enum LightingMode
{
	FullyRealtime,
	RealtimeGI,
	Baked
}
public enum LightSettings
{
	Default,
	Realtime,
	Mixed,
	Baked
}
public enum MyColorSpace
{
	Linear,
	Gamma
}
public enum VolumetricLightType
{
	Off,
	OnlyDirectional,
	AllLightSources
}
public enum VLightLevel
{
	Level1,	Level2,Level3,Level4
}
public enum CustomFog
{
	Off,Global,
	Height,
	Distance
}
public enum LightsShadow
{
	OnlyDirectionalSoft,OnlyDirectionalHard,
	AllLightsSoft,AllLightsHard,
	Off
}
public enum LightProbeMode
{
	Blend,
	Proxy
}
public enum Render_Path
{
	Default,Forward,Deferred
}
public enum DOFType
{
	On,Off
}
public enum AutoFocus
{
	Off,On
}
public enum DOFQuality
{
	Low,Medium,High
}
public enum AOMode
{
	On,Off
}
public enum AOType
{
	Classic,Modern
}
public enum BloomMode
{
	On,Off
}
public enum MBMode
{
	On,Off
}
public enum VTMode
{
	On,Off
}
public enum CAMode
{
	On,Off
}
public enum ColorMode
{
	ACES,Neutral
}
public enum SSRMode
{
	On,Off
}
public enum AAMode
{
	TAA,FXAA,SMAA,OFF
}
public enum ShaftMode
{
	Off,On
}
#endregion

public class LB_LightingBoxHelper : MonoBehaviour {
	
	public void UpdateProfiles(Camera[] cams,PostProcessProfile profile)
	{
		if (!profile)
			return;
		
		if (profile)
		{
			foreach (Camera c in cams) 
			{
				if (!c.GetComponent<PostProcessLayer> ())
				{
					c.gameObject.AddComponent<PostProcessLayer> ();
					c.gameObject.GetComponent<PostProcessLayer> ().antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
					c.gameObject.GetComponent<PostProcessLayer> ().volumeLayer = LayerMask.NameToLayer("Everything");
					c.gameObject.GetComponent<PostProcessLayer> ().fog.enabled = true;
					c.gameObject.GetComponent<PostProcessLayer> ().Init(null);
				}
			}
		}

		if (!GameObject.Find ("Global Volume")) {
			GameObject gVolume = new GameObject ();
			gVolume.name = "Global Volume";
			gVolume.AddComponent<PostProcessVolume> ();
			gVolume.GetComponent<PostProcessVolume> ().isGlobal = true;
			gVolume.GetComponent<PostProcessVolume> ().priority = 1f;
			if (profile)
				gVolume.GetComponent<PostProcessVolume> ().sharedProfile = profile;
		} else {
			if (profile)
				GameObject.Find ("Global Volume").GetComponent<PostProcessVolume> ().sharedProfile = profile;
		}

	}

	public void Update_MotionBlur(bool enabled)
	{
		MotionBlur mb;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out mb);
		mb.enabled.value = enabled; 
	}

	public void Update_Vignette(bool enabled)
	{
		Vignette vi;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out vi);
		vi.enabled.value = enabled; 
	}

	public void Update_ChromaticAberration(bool enabled)
	{
		ChromaticAberration ca;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out ca);
		ca.enabled.value = enabled; 
	}

	public void Update_Bloom(bool enabled,float intensity,float threshold,Color color)
	{
		if(enabled)
		{
			Bloom b;
			GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out b);
			b.intensity.overrideState = true;
			b.intensity.value = intensity;
			b.threshold.overrideState = true;
			b.threshold.value = threshold;
			b.color.overrideState = true;
			b.color.value = color;
			b.enabled.value = true;
		}
		else
		{
			Bloom b;
			GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out b);
			b.intensity.overrideState = true;
			b.intensity.value = intensity;
			b.threshold.overrideState = true;
			b.threshold.value = threshold;
			b.color.overrideState = true;
			b.color.value = color;
			b.enabled.value = false;
		}
	}

	public void Update_AutoFocus(Camera[] cams , DOFType dofType, AutoFocus autoFocus,LayerMask afLayer,float afRange,float afBlur,float afSpeed, float afUpdate, float afRayLength)
	{
		

		if (dofType == DOFType.On) 
		{
			
			foreach (Camera c in cams) {
				if (autoFocus == AutoFocus.On)
				{
					if (!c.GetComponent<LB_AutoFocus> ()) {
						c.gameObject.AddComponent<LB_AutoFocus> ();
						LB_AutoFocus dofAF = c.GetComponent<LB_AutoFocus> ();

						dofAF.layerMask = afLayer;
						dofAF.maxRange = afRange;
						dofAF.maxBlur = afBlur;
						dofAF.speed = afSpeed;
						dofAF.updateInterval = afUpdate;
						dofAF.rayLength = afRayLength;

					}
					else 
					{
						LB_AutoFocus dofAF = c.GetComponent<LB_AutoFocus> ();

						dofAF.layerMask = afLayer;
						dofAF.maxRange = afRange;
						dofAF.maxBlur = afBlur;
						dofAF.speed = afSpeed;
						dofAF.updateInterval = afUpdate;
						dofAF.rayLength = afRayLength;
					}
				}
				if (autoFocus == AutoFocus.Off) {
					if (c.GetComponent<LB_AutoFocus> ())
						DestroyImmediate (c.GetComponent<LB_AutoFocus> ());

				}
			}
		} else {
			
			foreach (Camera c in cams) {
				if(c.GetComponent<LB_AutoFocus>())
					DestroyImmediate(c.GetComponent<LB_AutoFocus>());
			}
		}


	}

	public void Update_DOF(Camera[] cams ,DOFType dofType,DOFQuality quality,float blur,float range,float falloff, bool visualize)
	{
		foreach(Camera c in cams)
		{
			if (dofType == DOFType.On) {
				if (!c.GetComponent<LightingBox.Effects.DepthOfField> ()) {
					c.gameObject.AddComponent<LightingBox.Effects.DepthOfField> ();
					LightingBox.Effects.DepthOfField dof = c.GetComponent<LightingBox.Effects.DepthOfField> ();
					dof.settings.tweakMode = LightingBox.Effects.DepthOfField.TweakMode.Range;

					if(quality == DOFQuality.Low)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.Low;
					if(quality == DOFQuality.Medium)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.Medium;
					if(quality == DOFQuality.High)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.High;
					
					dof.settings.apertureShape = LightingBox.Effects.DepthOfField.ApertureShape.Octogonal;
					dof.focus.focusPlane = 0f;
					dof.focus.farFalloff = falloff;
					dof.focus.range = range;
					dof.focus.farBlurRadius = blur;
					dof.settings.visualizeFocus = visualize;
				} else {
					LightingBox.Effects.DepthOfField dof = c.GetComponent<LightingBox.Effects.DepthOfField> ();
					dof.settings.tweakMode = LightingBox.Effects.DepthOfField.TweakMode.Range;

					if(quality == DOFQuality.Low)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.Low;
					if(quality == DOFQuality.Medium)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.Medium;
					if(quality == DOFQuality.High)
						dof.settings.filteringQuality = LightingBox.Effects.DepthOfField.QualityPreset.High;

					dof.settings.apertureShape = LightingBox.Effects.DepthOfField.ApertureShape.Octogonal;
					dof.focus.focusPlane = 0f;
					dof.focus.farFalloff = falloff;
					dof.focus.range = range;
					dof.focus.farBlurRadius = blur;
					dof.settings.visualizeFocus = visualize;
				}
			}
			if (dofType == DOFType.Off) {
				if (c.GetComponent<LightingBox.Effects.DepthOfField> ())
					DestroyImmediate(c.GetComponent<LightingBox.Effects.DepthOfField> ());

			}
		}
	}

	public void Update_AA(Camera[] cams ,AAMode aaMode)
	{
		foreach (Camera c in cams)
		{
			if(aaMode == AAMode.TAA)
			{
				c.GetComponent<PostProcessLayer> ().antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
				c.GetComponent<PostProcessLayer> ().Init (null);
			}
			if(aaMode == AAMode.FXAA)
			{
				c.GetComponent<PostProcessLayer> ().antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
				c.GetComponent<PostProcessLayer> ().Init (null);
			}
			if(aaMode == AAMode.SMAA)
			{
				c.GetComponent<PostProcessLayer> ().antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
				c.GetComponent<PostProcessLayer> ().Init (null);
			}
			if(aaMode == AAMode.OFF)
			{
				c.GetComponent<PostProcessLayer> ().antialiasingMode = PostProcessLayer.Antialiasing.None;
				c.GetComponent<PostProcessLayer> ().Init (null);
			}
		}
	}

	public void Update_AO(Camera[] cams ,bool enabled,AOType aoType,float aoRadius,float aoIntensity,bool ambientOnly,Color aoColor, AmbientOcclusionQuality aoQuality)
	{

		AmbientOcclusion ao;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out ao);

		foreach (Camera c in cams) 
		{
			if (enabled) {
				if (aoType == AOType.Classic) {
					ao.enabled.overrideState = true;
					ao.enabled.value = true;
					ao.mode.overrideState = true;
					ao.mode.value = AmbientOcclusionMode.ScalableAmbientObscurance;
					ao.radius.overrideState = true;
					ao.radius.value = aoRadius;
					ao.ambientOnly.overrideState = true;
					ao.ambientOnly.value = ambientOnly;
					ao.color.overrideState = true;
					ao.color.value = aoColor;
					ao.intensity.overrideState = true;
					ao.intensity.value = aoIntensity;
					ao.quality.overrideState = true;
					ao.quality.value = aoQuality;
				}
				if (aoType == AOType.Modern) {		
					ao.enabled.overrideState = true;
					ao.enabled.value = true;
					ao.mode.overrideState = true;
					ao.mode.value = AmbientOcclusionMode.MultiScaleVolumetricObscurance;
					ao.radius.overrideState = true;
					ao.radius.value = aoRadius;
					ao.ambientOnly.overrideState = true;
					ao.ambientOnly.value = ambientOnly;
					ao.color.overrideState = true;
					ao.color.value = aoColor;
					ao.intensity.overrideState = true;
					ao.intensity.value = aoIntensity;
				}
			} else {
				ao.enabled.overrideState = true;
				ao.enabled.value = false;
			}

		}
	}

	public void Update_ColorGrading(ColorMode colorMode,float exposureIntensity,float contrastValue,float temp,float eyeKeyValue
		,float saturation,Color colorGamma,Color colorLift,float gamma, float lift)
	{
		ColorGrading cg;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out cg);

		AutoExposure ae;
		GameObject.Find("Global Volume").GetComponent<PostProcessVolume>().sharedProfile.TryGetSettings(out ae);

			cg.gradingMode.value = GradingMode.HighDefinitionRange;
			if (colorMode == ColorMode.ACES) {
				cg.tonemapper.overrideState = true;
				cg.tonemapper.value = Tonemapper.ACES;
			}
			if (colorMode == ColorMode.Neutral) {
				cg.tonemapper.overrideState = true;
				cg.tonemapper.value = Tonemapper.Neutral;
			}

			cg.lift.overrideState = true;
			cg.lift.value.Set (colorLift.r, colorLift.g, colorLift.b, lift);

			cg.gamma.overrideState = true;
			cg.gamma.value.Set (colorGamma.r, colorGamma.g, colorGamma.b, gamma);

			cg.gain.overrideState = true;
			cg.gain.value.Set (cg.gain.value.x, cg.gain.value.y, cg.gain.value.z, 0);

			cg.saturation.overrideState = true;
			cg.saturation.value = saturation * 100;

			cg.saturation.overrideState = true;
			cg.saturation.value = saturation * 100;
			cg.postExposure.overrideState = true;
			cg.postExposure.value = exposureIntensity;
			cg.contrast.overrideState = true;
			cg.contrast.value = contrastValue * 100;
			cg.temperature.overrideState = true;
			cg.temperature.value = temp;
			cg.enabled.value = true;

			ae.keyValue.value = eyeKeyValue;
			ae.enabled.value = true;
	}

	public void Update_SSR(Camera[] cams ,SSRMode ssrMode,float ssrBlur, int interCount)
	{

		if (ssrMode == SSRMode.On) 
		{
			foreach (Camera sc in cams) 
			{
				if (!sc.GetComponent<ScreenSpaceReflection> ())
				{
					sc.gameObject.AddComponent<ScreenSpaceReflection> ();
					ScreenSpaceReflection ssrR = sc.GetComponent<ScreenSpaceReflection> ();
					ssrR.enabled = true;
					ssrR.settings.reflectionSettings.reflectionQuality = ScreenSpaceReflection.SSRResolution.Low;
					ssrR.settings.reflectionSettings.maxDistance = 300f;
					ssrR.settings.reflectionSettings.iterationCount = interCount;
					ssrR.settings.reflectionSettings.stepSize = 7;
					ssrR.settings.reflectionSettings.widthModifier = 10f;
					ssrR.settings.reflectionSettings.reflectionBlur = 1f;
					ssrR.settings.reflectionSettings.reflectBackfaces = true;
					ssrR.settings.reflectionSettings.reflectionBlur = ssrBlur;

					ssrR.settings.intensitySettings.reflectionMultiplier = 1f;
					ssrR.settings.intensitySettings.fadeDistance = 0;
					ssrR.settings.intensitySettings.fresnelFade = 0;
					ssrR.settings.intensitySettings.fresnelFadePower = 1f;
				} else {
					ScreenSpaceReflection ssrR = sc.GetComponent<ScreenSpaceReflection> ();
					ssrR.enabled = true;
					ssrR.settings.reflectionSettings.reflectionQuality = ScreenSpaceReflection.SSRResolution.Low;
					ssrR.settings.reflectionSettings.maxDistance = 300f;
					ssrR.settings.reflectionSettings.iterationCount = interCount;
					ssrR.settings.reflectionSettings.stepSize = 7;
					ssrR.settings.reflectionSettings.widthModifier = 10f;
					ssrR.settings.reflectionSettings.reflectionBlur = 1f;
					ssrR.settings.reflectionSettings.reflectBackfaces = true;
					ssrR.settings.reflectionSettings.reflectionBlur = ssrBlur;

					ssrR.settings.intensitySettings.reflectionMultiplier = 1f;
					ssrR.settings.intensitySettings.fadeDistance = 0;
					ssrR.settings.intensitySettings.fresnelFade = 0;
					ssrR.settings.intensitySettings.fresnelFadePower = 1f;
				}
			}
		}

		if (ssrMode == SSRMode.Off) 
		{
			foreach (Camera sc in cams) 
			{
				if (sc.GetComponent<ScreenSpaceReflection> ())
					DestroyImmediate (sc.GetComponent<ScreenSpaceReflection> ());
			}
		}
	}

	public void Update_SkyBox(Material material)
	{
		if(material)
			RenderSettings.skybox = material;
		
	}

	public void Update_LightingMode(LightingMode lightingMode)
	{
		#if UNITY_EDITOR
		if (lightingMode == LightingMode.RealtimeGI) {
			Lightmapping.realtimeGI = true;
			Lightmapping.bakedGI = false;
			LightmapEditorSettings.giBakeBackend = LightmapEditorSettings.GIBakeBackend.Radiosity;
		}
		if (lightingMode == LightingMode.Baked)
		{
			Lightmapping.realtimeGI = false;
			Lightmapping.bakedGI = true;
			LightmapEditorSettings.giBakeBackend = LightmapEditorSettings.GIBakeBackend.PathTracer;
		}
		if (lightingMode == LightingMode.FullyRealtime) {
			Lightmapping.realtimeGI = false;
			Lightmapping.bakedGI = false;
		}
		#endif
	}

	public void Update_Ambient(AmbientLight ambientLight,Color ambientColor)
	{
		if (ambientLight == AmbientLight.Color) 
		{
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
			RenderSettings.ambientLight = ambientColor;
		}
		else
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
		
	}

	#if UNITY_EDITOR
	public void Update_LightSettings(LightSettings lightSettings)
	{
		if (lightSettings == LightSettings.Baked) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				SerializedObject serialLightSource = new SerializedObject(l);
				SerializedProperty SerialProperty  = serialLightSource.FindProperty("m_Lightmapping");
				SerialProperty.intValue = 2;
				serialLightSource.ApplyModifiedProperties ();
			}
		} 
		if (lightSettings == LightSettings.Realtime) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				SerializedObject serialLightSource = new SerializedObject(l);
				SerializedProperty SerialProperty  = serialLightSource.FindProperty("m_Lightmapping");
				SerialProperty.intValue = 4;
				serialLightSource.ApplyModifiedProperties ();
			}
		}
		if (lightSettings == LightSettings.Mixed) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				SerializedObject serialLightSource = new SerializedObject(l);
				SerializedProperty SerialProperty  = serialLightSource.FindProperty("m_Lightmapping");
				SerialProperty.intValue = 1;
				serialLightSource.ApplyModifiedProperties ();
			}

		}
	}


	public void Update_ColorSpace(MyColorSpace colorSpace)
	{
		if (colorSpace == MyColorSpace.Gamma) 
			PlayerSettings.colorSpace = ColorSpace.Gamma;
		else
			PlayerSettings.colorSpace = ColorSpace.Linear;
	}

	public void Update_AutoMode(bool enabled)
	{
		if(enabled)
			Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.Iterative;
		else
			Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
	}
	public void Update_LightProbes(LightProbeMode lightProbesMode)
	{
		if (lightProbesMode == LightProbeMode.Blend) {

			MeshRenderer[] renderers = GameObject.FindObjectsOfType<MeshRenderer> ();

			foreach (MeshRenderer mr in renderers) 
			{
				if (!mr.gameObject.isStatic) {
					if (mr.gameObject.GetComponent<LightProbeProxyVolume> ())
						DestroyImmediate (mr.gameObject.GetComponent<LightProbeProxyVolume> ());
					mr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.BlendProbes;
				}
			}
		}
		if (lightProbesMode == LightProbeMode.Proxy) {

			MeshRenderer[] renderers = GameObject.FindObjectsOfType<MeshRenderer> ();

			foreach (MeshRenderer mr in renderers) {

				if (!mr.gameObject.isStatic) {
					if(!mr.gameObject.GetComponent<LightProbeProxyVolume> ())
						mr.gameObject.AddComponent<LightProbeProxyVolume> ();
					mr.gameObject.GetComponent<LightProbeProxyVolume> ().resolutionMode = LightProbeProxyVolume.ResolutionMode.Custom;
					mr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.UseProxyVolume;
				}
			}
		}
	}

	public void Update_Shadows(LightsShadow lightsShadow)
	{
		if (lightsShadow == LightsShadow.AllLightsSoft) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.type == LightType.Directional)
					l.shadows = LightShadows.Soft;

				if (l.type == LightType.Spot)
					l.shadows = LightShadows.Soft;

				if (l.type == LightType.Point)
					l.shadows = LightShadows.Soft;
			}
		}
		if (lightsShadow == LightsShadow.AllLightsHard) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.type == LightType.Directional)
					l.shadows = LightShadows.Hard;

				if (l.type == LightType.Spot)
					l.shadows = LightShadows.Hard;

				if (l.type == LightType.Point)
					l.shadows = LightShadows.Hard;
			}
		}
		if (lightsShadow == LightsShadow.OnlyDirectionalSoft) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.type == LightType.Directional)
					l.shadows = LightShadows.Soft;

				if (l.type == LightType.Spot)
					l.shadows = LightShadows.None;

				if (l.type == LightType.Point)
					l.shadows = LightShadows.None;
			}
		}
		if (lightsShadow == LightsShadow.OnlyDirectionalHard) {

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.type == LightType.Directional)
					l.shadows = LightShadows.Hard;

				if (l.type == LightType.Spot)
					l.shadows = LightShadows.None;

				if (l.type == LightType.Point)
					l.shadows = LightShadows.None;
			}
		}
		if (lightsShadow == LightsShadow.Off) {
			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) 
			{
				if (l.type == LightType.Directional)
					l.shadows = LightShadows.Hard;

				if (l.type == LightType.Spot)
					l.shadows = LightShadows.None;

				if (l.type == LightType.Point)
					l.shadows = LightShadows.None;
			}
		}
	}

	#endif

	public void Update_RenderPath(Render_Path renderPath,Camera[] allCameras)
	{
		foreach(Camera c in allCameras)
		{
			if (renderPath == Render_Path.Forward) 
				c.renderingPath = RenderingPath.Forward;
			if (renderPath == Render_Path.Deferred) 
				c.renderingPath = RenderingPath.DeferredShading;
			if (renderPath == Render_Path.Default) 
				c.renderingPath = RenderingPath.UsePlayerSettings;

			c.allowHDR = true;
			c.allowMSAA = false;
			c.useOcclusionCulling = true;

		}
	}

	public void Update_SunShaft(Camera[] cams,ShaftMode shaftMode, UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution shaftQuality,float shaftDistance,float shaftBlur, Color shaftColor, Transform sun)
	{
		if (!sun) 
		{
			Debug.Log("Couldn't find sun for Sun Shaft effect");
			return;
		}

		if (shaftMode == ShaftMode.On) 
		{
			foreach (Camera c in cams)
			{
				if (!c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ())
					c.gameObject.AddComponent<UnityStandardAssets.ImageEffects.SunShafts> ();
				
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().resolution = shaftQuality;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().screenBlendMode = UnityStandardAssets.ImageEffects.SunShafts.ShaftsScreenBlendMode.Screen;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftIntensity = 1f;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunThreshold = Color.black;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunColor = shaftColor;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunShaftBlurRadius = shaftBlur;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().radialBlurIterations = 2;
				c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().maxRadius = shaftDistance;
				if (!GameObject.Find ("Shaft Caster")) {
					GameObject shaftCaster = new GameObject ("Shaft Caster");
					shaftCaster.transform.parent = sun;
					shaftCaster.transform.localPosition = new Vector3 (0, 0, -7000f);
					c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunTransform = shaftCaster.transform;
				} else {
					GameObject.Find ("Shaft Caster").transform.parent = sun;
					GameObject.Find ("Shaft Caster").transform.localPosition = new Vector3 (0, 0, -7000f);
					c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ().sunTransform = GameObject.Find ("Shaft Caster").transform;
				}
			}
		} 
		else 
		{
			foreach (Camera c in cams) 
			{
				if (c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ())
					DestroyImmediate(c.gameObject.GetComponent<UnityStandardAssets.ImageEffects.SunShafts> ());
			}
		}
	}

	public void Update_VolumetricLight(Camera[] cams,VolumetricLightType volumetricLight,VLightLevel vLightLevel)
	{
		if (volumetricLight != VolumetricLightType.Off) {


			foreach (Camera c in cams) {

				if (!c.gameObject.GetComponent<VolumetricLightRenderer> ()) {
					c.gameObject.AddComponent<VolumetricLightRenderer> ();
					c.gameObject.GetComponent<VolumetricLightRenderer> ().Resolution = VolumetricLightRenderer.VolumtericResolution.Quarter;
					c.gameObject.GetComponent<VolumetricLightRenderer> ().DefaultSpotCookie = Resources.Load ("spot_Cookie_") as Texture;
				}
			}

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (!l.gameObject.GetComponent<VolumetricLight> ())
					l.gameObject.AddComponent<VolumetricLight> ();

				l.gameObject.GetComponent<VolumetricLight> ().SampleCount = 8;
				if (l.type == LightType.Directional) {
					if (vLightLevel == VLightLevel.Level1)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.0007f;
					if (vLightLevel == VLightLevel.Level2)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.001f;
					if (vLightLevel == VLightLevel.Level3)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.003f;
					if (vLightLevel == VLightLevel.Level4)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.0043f;
				}
				else
				{
					if(vLightLevel == VLightLevel.Level1)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.021f;
					if(vLightLevel == VLightLevel.Level2)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.073f;
					if(vLightLevel == VLightLevel.Level3)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.1f;
					if(vLightLevel == VLightLevel.Level4)
						l.gameObject.GetComponent<VolumetricLight> ().ScatteringCoef = 0.21f;
				}

				l.gameObject.GetComponent<VolumetricLight> ().ExtinctionCoef = 0;
				l.gameObject.GetComponent<VolumetricLight> ().SkyboxExtinctionCoef = 0.864f;
				l.gameObject.GetComponent<VolumetricLight> ().MieG = 0.675f;
				l.gameObject.GetComponent<VolumetricLight> ().HeightFog = false;
				l.gameObject.GetComponent<VolumetricLight> ().HeightScale = 0.1f;
				l.gameObject.GetComponent<VolumetricLight> ().GroundLevel = 0;
				if (l.type == LightType.Directional)
					l.gameObject.GetComponent<VolumetricLight> ().Noise = false;
				else {
					l.gameObject.GetComponent<VolumetricLight> ().Noise = true;

					if (l.type == LightType.Spot) {
						if (l.range == 10f)
							l.range = 43f;
						if (l.spotAngle == 30f)
							l.spotAngle = 43f;
					}
				}

				l.gameObject.GetComponent<VolumetricLight> ().NoiseScale = 0.015f;
				l.gameObject.GetComponent<VolumetricLight> ().NoiseIntensity = 1f;
				l.gameObject.GetComponent<VolumetricLight> ().NoiseIntensityOffset = 0.3f;
				l.gameObject.GetComponent<VolumetricLight> ().NoiseVelocity = new Vector2 (3f, 3f);
				l.gameObject.GetComponent<VolumetricLight> ().MaxRayLength = 400;
			}
		}
		if (volumetricLight == VolumetricLightType.Off) {

			foreach (Camera c in cams) {
				if (c.gameObject.GetComponent<VolumetricLightRenderer> ())
					DestroyImmediate (c.gameObject.GetComponent<VolumetricLightRenderer> ());
			}

			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.gameObject.GetComponent<VolumetricLight> ())
					DestroyImmediate(l.gameObject.GetComponent<VolumetricLight> ());
			}
		}
	}

	public void Update_GlobalFog(Camera[] cams,CustomFog fogMode,float fogDistance,float fogHeight,float fogHeightDensity,Color fogColor,float fogDensity)
	{
		if (fogMode == CustomFog.Distance)
			UpdateFog(cams,1,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		if (fogMode == CustomFog.Global)
			UpdateFog(cams,2,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		//-----Height--------------------------------------------------------------------
		if (fogMode == CustomFog.Height)
			UpdateFog(cams,0,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		//-----Global Fog Type--------------------------------------------------------------------

		if (fogMode == CustomFog.Height)
			UpdateFog(cams,0,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		if (fogMode == CustomFog.Distance) 
			UpdateFog(cams,1,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		if (fogMode == CustomFog.Global)
			UpdateFog(cams,2,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
		if (fogMode == CustomFog.Off)
			UpdateFog(cams,3,fogDistance,fogHeight,fogHeightDensity,fogColor,fogDensity);
	}

	private void UpdateFog(Camera[] cams,int fogType,float fogDistance,float fogHeight,float fogHeightDensity,Color fogColor,float fogDensity) // 0 Height , 1 Distance , 2 Global , 3 Off
	{

		//-------Height---------------------------------------------------------------------
		if (fogType == 0) {

			foreach (Camera c in cams) {
				if (!c.gameObject.GetComponent<GlobalFog> ())
				{
					c.gameObject.AddComponent<GlobalFog> ();
					c.gameObject.GetComponent<GlobalFog> ().fogShader = Shader.Find ("Hidden/GlobalFog");
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = false;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = true;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;

					c.gameObject.GetComponent<GlobalFog> ().heightDensity = fogHeightDensity;
					c.gameObject.GetComponent<GlobalFog> ().height = fogHeight;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = false;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;

				} else {
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = false;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = true;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;

					c.gameObject.GetComponent<GlobalFog> ().heightDensity = fogHeightDensity;
					c.gameObject.GetComponent<GlobalFog> ().height = fogHeight;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = false;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;

				}
			}
		}
		//----------------------------------------------------------------------------

		//-------Distance---------------------------------------------------------------------
		if (fogType == 1) {

			foreach (Camera c in cams) {
				if (!c.gameObject.GetComponent<GlobalFog> ())
				{
					c.gameObject.AddComponent<GlobalFog> ();
					c.gameObject.GetComponent<GlobalFog> ().fogShader = Shader.Find ("Hidden/GlobalFog");
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = true;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = false;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = true;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;
				} else {
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = true;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = false;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = true;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;
				}
			}
		}
		//----------------------------------------------------------------------------

		//-------Global---------------------------------------------------------------------
		if (fogType == 2) {

			foreach (Camera c in cams) {
				if (!c.gameObject.GetComponent<GlobalFog> ())
				{
					c.gameObject.GetComponent<PostProcessLayer> ().fog.enabled = true;
					c.gameObject.GetComponent<PostProcessLayer> ().fog.excludeSkybox = true;


					c.gameObject.AddComponent<GlobalFog> ();
					c.gameObject.GetComponent<GlobalFog> ().fogShader = Shader.Find ("Hidden/GlobalFog");
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = false;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = false;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = true;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;
				} else {
					c.gameObject.GetComponent<PostProcessLayer> ().fog.enabled = true;
					c.gameObject.GetComponent<PostProcessLayer> ().fog.excludeSkybox = true;
					c.gameObject.GetComponent<GlobalFog> ().distanceFog = false;
					c.gameObject.GetComponent<GlobalFog> ().heightFog = false;
					c.gameObject.GetComponent<GlobalFog> ().startDistance = fogDistance;
					c.gameObject.GetComponent<GlobalFog> ().useRadialDistance = true;
					c.gameObject.GetComponent<GlobalFog> ().excludeFarPixels = true;
					RenderSettings.fog = true;
					RenderSettings.fogColor = fogColor;
					RenderSettings.fogMode = FogMode.ExponentialSquared;
					RenderSettings.fogDensity = fogDensity/1000;
				}
			}
		}
		//----------------------------------------------------------------------------

		//-------Off---------------------------------------------------------------------
		if (fogType == 3) {

			foreach (Camera c in cams) 
			{
				if (c.gameObject.GetComponent<GlobalFog> ()) {
					DestroyImmediate (c.gameObject.GetComponent<GlobalFog> ());
					RenderSettings.fog = false;
				}
			}
		}
		//----------------------------------------------------------------------------
	}

	public void Update_Sun(Light sunLight,Color sunColor,float indirectIntensity)
	{
		if (!RenderSettings.sun) 
		{
			Light[] lights = GameObject.FindObjectsOfType<Light> ();

			foreach (Light l in lights) {
				if (l.type == LightType.Directional) 
				{
					sunLight = l;

					if (sunColor != Color.clear)
						sunColor = sunLight.color;
					else
						sunColor = Color.white;

					sunLight.shadowNormalBias = 0.05f;  
					sunLight.color = sunColor;
					if (sunLight.bounceIntensity == 1f)
						sunLight.bounceIntensity = indirectIntensity;
				}
			}
		} else 
		{
			sunLight = RenderSettings.sun;

			if (sunColor != Color.clear)
				sunColor = sunLight.color;
			else
				sunColor = Color.white;

			sunLight.shadowNormalBias = 0.05f;  
			sunLight.color = sunColor;
			if (sunLight.bounceIntensity == 1f)
				sunLight.bounceIntensity = indirectIntensity;
		}
	}

	bool effectsIsOn = true;

	public void Toggle_Effects()
	{
		effectsIsOn = !effectsIsOn;

		// Post layers
		PostProcessLayer[] postLayers = GameObject.FindObjectsOfType<PostProcessLayer> ();
		for (int a = 0; a < postLayers.Length; a++)
			postLayers [a].enabled = effectsIsOn;

		// Depth of Field
		LightingBox.Effects.DepthOfField[] dofEffects = GameObject.FindObjectsOfType<LightingBox.Effects.DepthOfField> ();
		for (int a = 0; a < dofEffects.Length; a++)
			dofEffects [a].enabled = effectsIsOn;

		// Screen Space Reflection
		LightingBox.Effects.ScreenSpaceReflection[] ssrS = GameObject.FindObjectsOfType<LightingBox.Effects.ScreenSpaceReflection> ();
		for (int a = 0; a < ssrS.Length; a++)
			ssrS [a].enabled = effectsIsOn;

		// Global fog
		LightingBox.Effects.GlobalFog[] gFogS = GameObject.FindObjectsOfType<LightingBox.Effects.GlobalFog> ();
		for (int a = 0; a < gFogS.Length; a++)
			gFogS [a].enabled = effectsIsOn;

		// Sun Shaft
		UnityStandardAssets.ImageEffects.SunShafts[] sunShaftS = GameObject.FindObjectsOfType<UnityStandardAssets.ImageEffects.SunShafts> ();
		for (int a = 0; a < sunShaftS.Length; a++)
			sunShaftS [a].enabled = effectsIsOn;

		// Volumetric Light RendererS
		VolumetricLightRenderer[] vlRendererS = GameObject.FindObjectsOfType<VolumetricLightRenderer> ();
		for (int a = 0; a < vlRendererS.Length; a++)
			vlRendererS [a].enabled = effectsIsOn;
	}
}
