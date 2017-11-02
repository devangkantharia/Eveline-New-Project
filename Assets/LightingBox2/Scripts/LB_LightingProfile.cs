#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(fileName = "Lighting Data", menuName = "Lighting Profile", order = 1)]
public class LB_LightingProfile : ScriptableObject {

	public string objectName = "LB_LightingProfile";
	[Header("Pr4ofiles")]
	public PostProcessProfile postProcessingProfile;
	public bool webGL_Mobile = false;

	[Header("Global")]
	public Render_Path renderPath = Render_Path.Default;
	public  LightingMode lightingMode = LightingMode.RealtimeGI;
	public  float bakedResolution = 10f;
	public  LightSettings lightSettings = LightSettings.Mixed;
	public MyColorSpace colorSpace = MyColorSpace.Linear;

	[Header("Environment")]
	public Material skyBox;
	public  AmbientLight ambientLight = AmbientLight.Color;
	public  Color ambientColor = new Color32(96,104,116,255);

	[Header("Sun")]
	public  Color sunColor = Color.white;
	public float sunIntensity = 2.1f;
	public Flare sunFlare;
	public float indirectIntensity = 1.43f;

	[Header("Fog")]
	public CustomFog fogMode = CustomFog.Off;
	public float fogDistance = 0;
	public float fogHeight = 30f;
	public float fogHeightDensity = 0.5f;
	public Color fogColor = Color.white;
	public float fogDensity;
	public float brightness;

	[Header("Bloom")]
	public float bIntensity = 0.73f;
	public float bThreshould = 0.5f;
	public Color bColor = Color.white;
	public BloomMode bMode = BloomMode.On;

	[Header("AO")]
	public AOType aoType = AOType.Modern;
	public float aoRadius = 0.3f;
	public AOMode aoMode = AOMode.On;
	public float aoIntensity = 1f;
	public bool ambientOnly = false;
	public Color aoColor = Color.black;
	public AmbientOcclusionQuality aoQuality = AmbientOcclusionQuality.Medium;

	[Header("Other")]
	public VolumetricLightType vLight = VolumetricLightType.OnlyDirectional;
	public VLightLevel vLightLevel = VLightLevel.Level3;
	public LightsShadow lightsShadow = LightsShadow.OnlyDirectionalSoft;
	public LightProbeMode lightProbesMode;
	public bool automaticLightmap = false;

	[Header("Depth of Field")]
	public DOFType dofType;
	public float dofRange;
	public float dofBlur;
	public float falloff = 30f;
	public DOFQuality dofQuality;
	public bool visualize;

	// Auto Focus
	public AutoFocus autoFocus = AutoFocus.Off;
	public float afRange = 100f;
	public float afBlur = 30f;
	public float afSpeed = 100f;
	public float afUpdate = 0.001f;
	public float afRayLength = 10f;
	public LayerMask afLayer = 1;

	[Header("Color settings")]
	public float exposureIntensity = 1.43f;
	public float contrastValue = 30f;
	public float temp = 0;
	public ColorMode colorMode = ColorMode.ACES;
	public float saturation = 0;
	public float gamma = 0;
	public Color colorGamma = Color.black;
	public Color colorLift = Color.black;
	public float lift = 0f;

	[Header("Effects")]
	public MBMode mbMode = MBMode.On;
	public VTMode vtMode = VTMode.On;
	public CAMode caMode = CAMode.On;
	public SSRMode ssrMode = SSRMode.Off;
	public float ssrBlur;
	public int interCount = 1024;

	[Header("Sun Shaft")]
	public ShaftMode shaftMode = ShaftMode.Off;
	public UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution shaftQuality = UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution.High;
	public float shaftDistance = 0.5f;
	public float shaftBlur = 4f;
	public Color shaftColor = new Color32 (255,189,146,255);

	[Range(0,1f)]
	public float eyeKeyValue  =   0.17f;   

	[Header("AA")]
	public AAMode aaMode;

}
#endif