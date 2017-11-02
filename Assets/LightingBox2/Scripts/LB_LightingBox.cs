
// AliyerEdon@gmail.com/
// Lighting Box is an "paid" asset. Don't share it for free

#if UNITY_EDITOR   
using UnityEngine;   
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;   
using UnityEngine.Rendering.PostProcessing;
using LightingBox.Effects;
using SharpConfig;

[ExecuteInEditMode]
public class LB_LightingBox : EditorWindow
{
	#region Variables

	public WindowMode winMode = WindowMode.Part1;
	public LB_LightingBoxHelper helper;
	public bool webGL_Mobile = false;

	// Sun Shaft
	public ShaftMode shaftMode = ShaftMode.Off;
	public UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution shaftQuality = UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution.High;
	public float shaftDistance = 0.5f;
	public float shaftBlur = 4f;
	public Color shaftColor = new Color32 (255,189,146,255);

	// AA
	public AAMode aaMode;

	// AO
	public AOType aoType;
	public float aoRadius;
	public AOMode aoMode = AOMode.On;
	public float aoIntensity = 1f;
	public bool ambientOnly = true;
	public Color aoColor = Color.black;
	public AmbientOcclusionQuality aoQuality = AmbientOcclusionQuality.Medium;

	// Bloom
	public float bIntensity = 1f;
	public float bThreshould = 0.5f;
	public Color bColor = Color.white;
	public BloomMode bMode = BloomMode.On;
	public bool visualize;

	// Color settings
	public float exposureIntensity = 1.43f;
	public float contrastValue = 30f;
	public float temp = 0;
	public float eyeKeyValue = 0.17f;
	public ColorMode colorMode;
	public float gamma = 0;
	public Color colorGamma = Color.black;
	public Color colorLift = Color.black;
	public float saturation = 0;
	public float lift = 0f;

	// Effects
	public MBMode mbMode;
	public VTMode vtMode;
	public CAMode caMode;
	public SSRMode ssrMode;
	public float ssrBlur;
	public int interCount = 256;

	public Render_Path renderPath;

	// Profiles
	public LB_LightingProfile LB_LightingProfile;
	public PostProcessProfile postProcessingProfile;

	public LightingMode lightingMode;
	public AmbientLight ambientLight;
	public LightSettings lightSettings;
	public LightProbeMode lightprobeMode;

	// Depth of field
	public DOFType dofType;
	public float dofRange;
	public float dofBlur;
	public float falloff = 30f;
	public DOFQuality dofQuality;
	// Auto Focus
	public AutoFocus autoFocus = AutoFocus.Off;
	public float afRange = 100f;
	public float afBlur = 30f;
	public float afSpeed = 100f;
	public float afUpdate = 0.001f;
	public float afRayLength = 10f;
	public LayerMask afLayer = 1;

	// Sky and Sun
	public Material skyBox;
	public Light sunLight;
	public Flare sunFlare;
	public Color sunColor = Color.white;
	public float sunIntensity = 2.1f;
	public float indirectIntensity = 1.43f;
	public Color ambientColor;

	public bool autoMode;
	public MyColorSpace colorSpace;

	// Volumetric Light
	public VolumetricLightType vLight;
	public VLightLevel vLightLevel;

	// Volumetric Fog
	CustomFog vFog;
	float fDistance = 0;
	float fHeight = 30f;
	[Range(0,1)]
	float fheightDensity = 0.5f;
	Color fColor = Color.white;
	[Range(0,10)]
	float fDensity = 1f;

	public LightsShadow psShadow;
	public float bakedResolution = 10f;
	public bool helpBox;

	// Private variabled
	Color redColor;
	bool lightError;
	bool lightChecked;
	GUIStyle myFoldoutStyle;
	bool showLogs;
	// Display window elements (Lighting Box)   
	Vector2 scrollPos;

	// Camera
	public CameraMode camMode;
	public Camera[] cameraList;

	#endregion

	#region Init()
	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/Lighting Box 2 %E")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		LB_LightingBox window = (LB_LightingBox)EditorWindow.GetWindow(typeof(LB_LightingBox));
		window.Show();
		window.autoRepaintOnSceneChange = true;
	}
	#endregion

	void NewSceneInit()
	{
		if (EditorSceneManager.GetActiveScene ().name == "") 
		{
			LB_LightingProfile = Resources.Load ("DefaultSettings")as LB_LightingProfile;
			OnLoad ();
			currentScene = EditorSceneManager.GetActiveScene ().name;

		} 
		else
		{
			if (System.String.IsNullOrEmpty (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name))) 
			{
				LB_LightingProfile = Resources.Load ("DefaultSettings")as LB_LightingProfile;
			} else 
			{
				LB_LightingProfile = (LB_LightingProfile)AssetDatabase.LoadAssetAtPath (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name), typeof(LB_LightingProfile));
			}

			OnLoad ();
			currentScene = EditorSceneManager.GetActiveScene ().name;

		}


	}
	// Load and apply default settings when a new scene opened
	void OnNewSceneOpened()
	{
		NewSceneInit ();
	}

	void OnDisable()
	{
		EditorApplication.hierarchyWindowChanged -= OnNewSceneOpened;
	}



	void OnEnable()
	{

		if (!GameObject.Find ("LightingBox_Helper")) 
		{
			GameObject helperObject = new GameObject ("LightingBox_Helper");
			helperObject.AddComponent<LB_LightingBoxHelper> ();
			helper = helperObject.GetComponent<LB_LightingBoxHelper> ();
		}

		EditorApplication.hierarchyWindowChanged += OnNewSceneOpened;

		currentScene = EditorSceneManager.GetActiveScene().name;

		if (System.String.IsNullOrEmpty (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name)))
			LB_LightingProfile = Resources.Load ("DefaultSettings")as LB_LightingProfile;
		else
			LB_LightingProfile = (LB_LightingProfile)AssetDatabase.LoadAssetAtPath (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name), typeof(LB_LightingProfile));

		OnLoad ();

	}

	void OnGUI()
	{
		GUIStyle redStyle = new GUIStyle(EditorStyles.label);
		redStyle.normal.textColor = Color.red;

		#region GUI start implementation
		Undo.RecordObject (this,"lb");

		scrollPos = EditorGUILayout.BeginScrollView (scrollPos,
			false,
			false,
			GUILayout.Width(Screen.width ),
			GUILayout.Height(Screen.height));

		EditorGUILayout.Space ();

		GUILayout.Label ("Lighting Box 2 - ALIyerEdon@gmail.com", EditorStyles.helpBox);

		UpdateCamera();

		if (!helpBox) 
		{
			if (GUILayout.Button ("Show Help")) {
				helpBox = !helpBox;
			}
		} else 
		{
			if (GUILayout.Button ("Hide Help")) {
				helpBox = !helpBox;
			}
		}
		if (helpBox)
			EditorGUILayout.HelpBox("Update PlayMode changes after stop the game",MessageType.Info);

		if (GUILayout.Button ("Refresh"))
		{
			UpdateSettings();
			UpdatePostEffects();
		}


		if (EditorPrefs.GetInt ("RateLB") != 3) {

			if (GUILayout.Button ("Rate Lighting Box")) {
				EditorPrefs.SetInt ("RateLB", 3);
				Application.OpenURL ("http://u3d.as/Se9");
			}
		}

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		#endregion

		#region Tabs
		EditorGUILayout.BeginHorizontal();
		//----------------------------------------------
		if(winMode == WindowMode.Part1)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.white;
		//----------------------------------------------
		if(GUILayout.Button("Part 1",GUILayout.Width (73), GUILayout.Height (43)))
			winMode = WindowMode.Part1;
		//----------------------------------------------
		if(winMode == WindowMode.Part2)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.white;
		//----------------------------------------------
		if(GUILayout.Button("Part 2",GUILayout.Width (73) , GUILayout.Height (43)))
			winMode = WindowMode.Part2;
		//----------------------------------------------
		if(winMode == WindowMode.Part3)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.white;
		//----------------------------------------------
		if(GUILayout.Button("Part 3",GUILayout.Width (73), GUILayout.Height (43)))
			winMode = WindowMode.Part3;
		//----------------------------------------------
		if(winMode == WindowMode.Finish)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.white;
		//----------------------------------------------
		if(GUILayout.Button("Finish",GUILayout.Width (73), GUILayout.Height (43)))
			winMode = WindowMode.Finish;
		//----------------------------------------------
		GUI.backgroundColor = Color.white;
		//----------------------------------------------//----------------------------------------------//----------------------------------------------

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		GUILayout.Box ("", new GUILayoutOption[]{ GUILayout.ExpandWidth (true), GUILayout.Height (1) });
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		#endregion

		if (winMode == WindowMode.Part1) 
		{

			#region Camera

			if (helpBox)
				EditorGUILayout.HelpBox ("Which camera should has effects. Useful for multiple camera with different layers in the scene. for example for weather systems like Enviro", MessageType.Info);

			var camsRef = cameraList;

			camMode = (CameraMode)EditorGUILayout.EnumPopup("Camera Mode",camMode,GUILayout.Width(343));
			EditorGUILayout.Space ();
			/*	if(GUILayout.Button("Clear Components"))
			{
				Light[] ls = GameObject.FindObjectsOfType<Light>();
				Camera[] ccc = GameObject.FindObjectsOfType<Camera>();
				foreach(Camera cc in ccc)
				{
					if(cc.GetComponent<LightingBox.Effects.ScreenSpaceReflection>())
						DestroyImmediate(cc.GetComponent<LightingBox.Effects.ScreenSpaceReflection>());
					if(cc.GetComponent<LightingBox.Effects.DepthOfField>())
						DestroyImmediate(cc.GetComponent<LightingBox.Effects.DepthOfField>());
					if(cc.GetComponent<PostProcessLayer>())
						DestroyImmediate(cc.GetComponent<PostProcessLayer>());
					if(cc.GetComponent<VolumetricLightRenderer>())
						DestroyImmediate(cc.GetComponent<VolumetricLightRenderer>());
				}
				foreach(Light l in ls)
				{
					if(l.GetComponent<VolumetricLight>())
						DestroyImmediate(l.GetComponent<VolumetricLight>());
				}
			}*/

			if(camMode == CameraMode.Custom)
			{
				ScriptableObject target = this;
				SerializedObject so = new SerializedObject(target);
				SerializedProperty stringsProperty = so.FindProperty("cameraList");
				EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
				so.ApplyModifiedProperties(); // Remember to apply modified properties
			}

			if (camsRef != cameraList)
			{
				UpdateCamera();
				UpdatePostEffects ();
				UpdateSettings();
			}

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			var webGL_MobileRef = webGL_Mobile;

			webGL_Mobile = EditorGUILayout.Toggle("WebGL 2.0 Target",webGL_Mobile);

			if(webGL_MobileRef != webGL_Mobile)
			{
				if (LB_LightingProfile)
					LB_LightingProfile.webGL_Mobile = webGL_Mobile;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}

			EditorGUILayout.Space ();

			#endregion

			#region Toggle Settings

			if(GUILayout.Button("Toggle Effects On / Off"))
			{
				helper.Toggle_Effects();
			}
			GUILayout.Box ("", new GUILayoutOption[]{ GUILayout.ExpandWidth (true), GUILayout.Height (1) });
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			#endregion

			#region Profiles

			if (helpBox)
				EditorGUILayout.HelpBox ("1. LB_LightingBox settings profile   2.Post Processing Stack Profile", MessageType.Info);

			var lightingProfileRef = LB_LightingProfile;

			EditorGUILayout.BeginHorizontal();
			LB_LightingProfile = EditorGUILayout.ObjectField ("Lighting Profile", LB_LightingProfile, typeof(LB_LightingProfile), true) as LB_LightingProfile;

			if(GUILayout.Button("New",GUILayout.Width(43),GUILayout.Height(17)))
			{

				if(EditorSceneManager.GetActiveScene().name == "")
					EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

				string path = EditorUtility.SaveFilePanelInProject("Save As ...","Lighting_Profile_","asset","");

				if(path!="")
				{
					LB_LightingProfile = new LB_LightingProfile();
					AssetDatabase.CreateAsset(LB_LightingProfile,path);
					AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Resources.Load("DefaultSettings_LB")),path);
					LB_LightingProfile = (LB_LightingProfile)AssetDatabase.LoadAssetAtPath(path, typeof(LB_LightingProfile));
					AssetDatabase.Refresh();
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			if (lightingProfileRef != LB_LightingProfile)
			{
				OnLoad ();
				EditorPrefs.SetString (EditorSceneManager.GetActiveScene ().name, AssetDatabase.GetAssetPath (LB_LightingProfile));
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}

			var pProfileRef = postProcessingProfile;
			EditorGUILayout.BeginHorizontal();

			postProcessingProfile = EditorGUILayout.ObjectField ("Post Processing Profile", postProcessingProfile, typeof(PostProcessProfile), true) as PostProcessProfile;
			if(GUILayout.Button("New",GUILayout.Width(43),GUILayout.Height(17)))
			{

				if(EditorSceneManager.GetActiveScene().name == "")
					EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

				string path = EditorUtility.SaveFilePanelInProject("Save As ...","Post_Profile_","asset","");
				if(path!="")
				{
					postProcessingProfile = new PostProcessProfile();
					AssetDatabase.CreateAsset(postProcessingProfile,path);
					AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Resources.Load("Default_Post_Profile")),path);
					postProcessingProfile = (PostProcessProfile)AssetDatabase.LoadAssetAtPath(path, typeof(PostProcessProfile));
					AssetDatabase.Refresh();
				}
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			if (pProfileRef != postProcessingProfile) {
				if (LB_LightingProfile)
					LB_LightingProfile.postProcessingProfile = postProcessingProfile;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
				UpdatePostEffects ();
			}
			EditorGUILayout.Space ();
			GUILayout.Box ("", new GUILayoutOption[]{ GUILayout.ExpandWidth (true), GUILayout.Height (1) });
			EditorGUILayout.Space ();

			#endregion

			#region Ambient

			if (helpBox)
				EditorGUILayout.HelpBox("Assign scene skybox material here   ",MessageType.Info);

			var skyboxRef = skyBox;

			skyBox = EditorGUILayout.ObjectField ("SkyBox Material", skyBox, typeof(Material), true) as Material;

			if (skyboxRef != skyBox)
			{

				helper.Update_SkyBox(skyBox);

				if (LB_LightingProfile)
					LB_LightingProfile.skyBox = skyBox;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}


			if (helpBox)
				EditorGUILayout.HelpBox("Set ambient lighting source as Skybox(IBL) or a simple color",MessageType.Info);

			var ambientLightRef = ambientLight;

			// choose ambient lighting mode   (color or skybox)
			ambientLight = (AmbientLight)EditorGUILayout.EnumPopup("Ambient Source",ambientLight,GUILayout.Width(343));

			if (ambientLight == AmbientLight.Color)
			{
				var ambientColorRef = ambientColor;

				ambientColor = EditorGUILayout.ColorField ("Color", ambientColor);

				if(ambientColorRef !=ambientColor)
				{


					helper.Update_Ambient(ambientLight,ambientColor);


					if(LB_LightingProfile) LB_LightingProfile.ambientColor = ambientColor;
					if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
				}
			}

			//----------------------------------------------------------------------
			// Update Ambient
			if(ambientLightRef != ambientLight)
			{

				helper.Update_Ambient(ambientLight,ambientColor);

				if(LB_LightingProfile) LB_LightingProfile.ambientColor = ambientColor;
				if(LB_LightingProfile) LB_LightingProfile.ambientLight = ambientLight;
				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);

			}
			//----------------------------------------------------------------------
			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Sun Light
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			if (helpBox)
				EditorGUILayout.HelpBox("Sun /  Moon light settings",MessageType.Info);

			EditorGUILayout.BeginHorizontal ();
			sunLight = EditorGUILayout.ObjectField ("Sun Light", sunLight, typeof(Light), true) as Light;
			if (!sunLight) {
				if (GUILayout.Button ("Find"))
					Update_Sun();
			}
			EditorGUILayout.EndHorizontal ();
			var sunColorRef = sunColor;

			sunColor = EditorGUILayout.ColorField ("Color", sunColor);

			var sunIntensityRef = sunIntensity;
			var indirectIntensityRef = indirectIntensity;

			sunIntensity = EditorGUILayout.Slider ("Intenity", sunIntensity,0,4f);
			indirectIntensity = EditorGUILayout.Slider ("Indirect Intensity", indirectIntensity,0,4f);

			var sunFlareRef = sunFlare;

			sunFlare = EditorGUILayout.ObjectField ("Lens Flare", sunFlare, typeof(Flare), true) as Flare;

			if (sunColorRef != sunColor) 
			{
				if (sunLight)
					sunLight.color = sunColor;
				else
					Update_Sun();		
				if (LB_LightingProfile)
					LB_LightingProfile.sunColor = sunColor;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}

			if (sunIntensityRef != sunIntensity || indirectIntensityRef != indirectIntensity) {
				if (sunLight)
				{
					sunLight.intensity = sunIntensity;
					sunLight.bounceIntensity = indirectIntensity;
				}
				else
					Update_Sun();
				if (LB_LightingProfile)
				{
					LB_LightingProfile.sunIntensity = sunIntensity;
					LB_LightingProfile.indirectIntensity = indirectIntensity;
				}
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			if (sunFlareRef != sunFlare) 
			{
				if (sunFlare)
				{
					if(sunLight)
						sunLight.flare = sunFlare;
				}
				else
				{
					if(sunLight)
						sunLight.flare = null;
				}

				if (LB_LightingProfile)
					LB_LightingProfile.sunFlare = sunFlare;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Lighting Mode

			if (helpBox)
				EditorGUILayout.HelpBox ("Fully realtime without GI, Enlighten Realtime GI or Baked Progressive Lightmapper", MessageType.Info);

			var lightingModeRef = lightingMode;

			// Choose lighting mode (realtime GI or baked GI)
			lightingMode = (LightingMode)EditorGUILayout.EnumPopup ("Lighting Mode", lightingMode, GUILayout.Width (343));

			if (lightingMode == LightingMode.Baked) 
			{
				EditorGUILayout.Space ();

				if (helpBox)
					EditorGUILayout.HelpBox ("Baked lightmapping resolution. Higher value needs more RAM and longer bake time. Check task manager about RAM usage during bake time", MessageType.Info);

				// Baked lightmapping resolution   
				bakedResolution = EditorGUILayout.FloatField ("Baked Resolution", bakedResolution);
				LightmapEditorSettings.bakeResolution = bakedResolution;
				if (LB_LightingProfile)
					LB_LightingProfile.bakedResolution = bakedResolution;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);

			}
			if(webGL_Mobile)
			{
				if (lightingMode == LightingMode.RealtimeGI)
				{
					EditorGUILayout.Space();
					EditorGUILayout.LabelField("Enlighten's Realtime GI is not available for WebGL platform",redStyle);
					EditorGUILayout.Space();

				}
			}

			if (lightingModeRef != lightingMode) {
				//----------------------------------------------------------------------
				// Update Lighting Mode
				helper.Update_LightingMode (lightingMode);
				//----------------------------------------------------------------------
				if (LB_LightingProfile)
					LB_LightingProfile.lightingMode = lightingMode;  
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			#endregion

			#region Color Space
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Choose between Linear or Gamma color space , default should be Linear for my settings and next-gen platfroms   ", MessageType.Info);

			var colorSpaceRef = colorSpace;

			// Choose color space (Linear,Gamma) i have used Linear in post effect setting s
			colorSpace = (MyColorSpace)EditorGUILayout.EnumPopup ("Color Space", colorSpace, GUILayout.Width (343));

			if (colorSpaceRef != colorSpace) {
				// Color Space
				helper.Update_ColorSpace (colorSpace);

				if (LB_LightingProfile)
					LB_LightingProfile.colorSpace = colorSpace;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			#endregion

			#region Render Path
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Choose between Forward and Deferred rendering path for cameras. Deferred needed for Screen Spacwe Reflection effect. Forward has better performance in unity", MessageType.Info);

			var renderPathRef = renderPath;

			renderPath = (Render_Path)EditorGUILayout.EnumPopup ("Render Path", renderPath, GUILayout.Width (343));

			if (renderPathRef != renderPath) {

				helper.Update_RenderPath (renderPath, cameraList);

				if (LB_LightingProfile)
					LB_LightingProfile.renderPath = renderPath;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);

			}

			#endregion

			#region Light Types
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Changing the type of all light sources (Realtime,Baked,Mixed)", MessageType.Info);

			var lightSettingsRef = lightSettings;

			// Change file lightmapping type mixed,realtime baked
			lightSettings = (LightSettings)EditorGUILayout.EnumPopup ("Lights Type", lightSettings, GUILayout.Width (343));


			//----------------------------------------------------------------------
			// Light Types
			if (lightSettingsRef != lightSettings) {

				helper.Update_LightSettings (lightSettings);

				if (LB_LightingProfile)
					LB_LightingProfile.lightSettings = lightSettings;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			//----------------------------------------------------------------------
			#endregion

			#region Light Shadows Settings
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Activate shadows for point and spot lights   ", MessageType.Info);

			var pshadRef = psShadow;
			// Choose hard shadows state on off for spot and point lights
			psShadow = (LightsShadow)EditorGUILayout.EnumPopup ("Shadows", psShadow, GUILayout.Width (343));

			if (pshadRef != psShadow) {

				// Shadows
				helper.Update_Shadows (psShadow);

				//----------------------------------------------------------------------
				if (LB_LightingProfile)
					LB_LightingProfile.lightsShadow = psShadow;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			#endregion

			#region Light Probes
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Adjust light probes settings for non-static objects, Blend mode is more optimized", MessageType.Info);

			var lightprobeModeRef = lightprobeMode;

			lightprobeMode = (LightProbeMode)EditorGUILayout.EnumPopup ("Light Probes", lightprobeMode, GUILayout.Width (343));

			if (lightprobeModeRef != lightprobeMode) {

				// Light Probes
				helper.Update_LightProbes (lightprobeMode);

				//----------------------------------------------------------------------
				if (LB_LightingProfile)
					LB_LightingProfile.lightProbesMode = lightprobeMode;
				if (LB_LightingProfile)
					EditorUtility.SetDirty (LB_LightingProfile);
			}
			EditorGUILayout.Space ();
			GUILayout.Box ("", new GUILayoutOption[]{ GUILayout.ExpandWidth (true), GUILayout.Height (1) });
			EditorGUILayout.Space ();

			#endregion

			#region Buttons

			var automodeRef = autoMode;

			if (helpBox)
				EditorGUILayout.HelpBox ("Automatic lightmap baking", MessageType.Info);


			autoMode = EditorGUILayout.Toggle ("Auto Mode", autoMode);

			if(automodeRef != autoMode)
			{
				// Auto Mode
				if(autoMode)
					Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.Iterative;
				else
					Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.automaticLightmap = autoMode;
				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			// Start bake
			if (!Lightmapping.isRunning) {

				if (helpBox)
					EditorGUILayout.HelpBox ("Bake lightmap", MessageType.Info);

				if (GUILayout.Button ("Bake")) 
				{
					if (!Lightmapping.isRunning) {
						Lightmapping.BakeAsync ();
					}
				}

				if (helpBox)
					EditorGUILayout.HelpBox ("Clear lightmap data", MessageType.Info);

				if(GUILayout.Button("Clear"))
				{
					Lightmapping.Clear ();
				}
			}else {

				if (helpBox)
					EditorGUILayout.HelpBox ("Cancel lightmap baking", MessageType.Info);

				if (GUILayout.Button ("Cancel")) {
					if (Lightmapping.isRunning) {
						Lightmapping.Cancel ();
					}
				}
			}

			if (Input.GetKey (KeyCode.F)) {
				if (Lightmapping.isRunning)
					Lightmapping.Cancel ();
			}
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey (KeyCode.E)) {
				if (!Lightmapping.isRunning)
					Lightmapping.BakeAsync ();
			}

			if (helpBox) {
				EditorGUILayout.HelpBox ("Bake : Shift + B", MessageType.Info);
				EditorGUILayout.HelpBox ("Cancel : Shift + C", MessageType.Info);
				EditorGUILayout.HelpBox ("Clear : Shift + E", MessageType.Info);

			}
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			if (helpBox)
				EditorGUILayout.HelpBox ("Open unity Lighting Settings window", MessageType.Info);

			if (GUILayout.Button ("Lighting Window")) {

				EditorApplication.ExecuteMenuItem("Window/Lighting/Settings");
			}

			if (helpBox)
				EditorGUILayout.HelpBox ("Debug scene and peoject lighting settings automaticity", MessageType.Info);

			if (GUILayout.Button ("Debug Lighting")) 
			{
				showLogs = !showLogs;
			}	

			EditorGUILayout.Space();EditorGUILayout.Space();

			if (GUILayout.Button ("Add Camera Move Script")) 
			{
				foreach(Camera c in cameraList)
				{
					if(!c.GetComponent<LB_CameraMove>())
						c.gameObject.AddComponent<LB_CameraMove>();
				}
			}
			if (GUILayout.Button ("Add RenderBox")) 
			{
				if(!GameObject.FindObjectOfType<LB_RenderBox>())
				{
					GameObject rb = new GameObject("RenderBox");
					rb.AddComponent<LB_RenderBox>();
					rb.GetComponent<LB_RenderBox>().screenShotResolution = SelectResolution._1080P;
				}
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Log System
			// Log window
			//===================================================
			if (showLogs) 
			{
				myFoldoutStyle = new GUIStyle(GUI.skin.button);
				redColor = new Color32 (184, 26, 26, 255);
				myFoldoutStyle.normal.textColor = redColor;
				myFoldoutStyle.fontStyle = FontStyle.Bold;
				myFoldoutStyle.fontStyle = FontStyle.Bold;

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				CheckColorSpace ();

				CheckLightingMode ();

				CheckSettings ();

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

			}
			EditorUtility.SetDirty (this);	
			EditorGUILayout.Space ();
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
			EditorGUILayout.Space ();


			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			#endregion

		}

		if (winMode == WindowMode.Part2) 
		{

			#region Volumetric Light
			EditorGUILayout.Space ();
			if (helpBox)
				EditorGUILayout.HelpBox("Activate Volumetric Lights For All Light Sources",MessageType.Info);

			var vLightRef = vLight;
			var vLightLevelRef = vLightLevel;

			// Activate or deactivate volumetric lighting for all light sources
			vLight = (VolumetricLightType)EditorGUILayout.EnumPopup("Volumetric Light",vLight,GUILayout.Width(343));

			if(vLight != VolumetricLightType.Off)
				vLightLevel = (VLightLevel)EditorGUILayout.EnumPopup("Intensity",vLightLevel,GUILayout.Width(343));

			if(vLightRef !=vLight || vLightLevelRef !=vLightLevel)
			{

				// Volumetric Light
				helper.Update_VolumetricLight(cameraList,vLight,vLightLevel);
				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.vLight = vLight;
				if(LB_LightingProfile) LB_LightingProfile.vLightLevel = vLightLevel;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			if(webGL_Mobile)
			{
				if(vLight != VolumetricLightType.Off)
				{
					EditorGUILayout.Space();
					EditorGUILayout.LabelField("Volumetric Light is not supported on WebGL platform",redStyle);
					EditorGUILayout.Space();

				}
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();


			#endregion

			#region Sun Shaft
			EditorGUILayout.Space ();
			if (helpBox)
				EditorGUILayout.HelpBox("Activate Sun Shaft for sun",MessageType.Info);

			var shaftModeRef = shaftMode;
			var shaftDistanceRef = shaftDistance;
			var shaftBlurRef = shaftBlur;
			var shaftColorRef = shaftColor;
			var shaftQualityRef = shaftQuality;

			// Activate Sun Shaft for sun
			shaftMode = (ShaftMode)EditorGUILayout.EnumPopup("Sun Shaft",shaftMode,GUILayout.Width(343));

			if(shaftMode != ShaftMode.Off)
			{
				shaftQuality = (UnityStandardAssets.ImageEffects.SunShafts.SunShaftsResolution)EditorGUILayout.EnumPopup("Quality",shaftQuality,GUILayout.Width(343));
				shaftDistance =  1.0f - EditorGUILayout.Slider ("Distance falloff", 1.0f - shaftDistance, 0.1f, 1.0f);
				shaftBlur = EditorGUILayout.Slider ("Blur", shaftBlur,1f,10f);
				shaftColor = (Color)EditorGUILayout.ColorField("Color",shaftColor);
			}

			if(shaftModeRef !=shaftMode || shaftDistanceRef !=shaftDistance
				|| shaftBlurRef !=shaftBlur || shaftColorRef !=shaftColor || shaftQualityRef != shaftQuality)
			{

				// Sun Shaft
				helper.Update_SunShaft(cameraList,shaftMode, shaftQuality,shaftDistance,shaftBlur,shaftColor,sunLight.transform);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.shaftMode = shaftMode;
				if(LB_LightingProfile) LB_LightingProfile.shaftQuality = shaftQuality;
				if(LB_LightingProfile) LB_LightingProfile.shaftDistance = shaftDistance;
				if(LB_LightingProfile) LB_LightingProfile.shaftBlur = shaftBlur;
				if(LB_LightingProfile) LB_LightingProfile.shaftColor = shaftColor;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Global Fog
			if (helpBox)
				EditorGUILayout.HelpBox("Activate Global Fog for the scene. Combined with unity Lighting Window Fog parameters",MessageType.Info);

			var vfogRef = vFog;

			vFog = (CustomFog)EditorGUILayout.EnumPopup("Global Fog",vFog,GUILayout.Width(343));

			//-----Distance--------------------------------------------------------------------
			if (vFog == CustomFog.Distance)
			{
				float fDistanceRef = fDistance;
				Color fColorRef = fColor;
				float fogDensityRef = fDensity;

				fDistance = (float)EditorGUILayout.FloatField("Start Distance",fDistance);
				fDensity = (float)EditorGUILayout.Slider("Density",fDensity,0,30f);
				fColor = (Color)EditorGUILayout.ColorField("Color",fColor);

				if(fDistanceRef != fDistance || fColorRef != fColor || fogDensityRef != fDensity )
				{
					helper.Update_GlobalFog(cameraList,vFog,fDistance,fHeight,fheightDensity,fColor,fDensity);
					if(LB_LightingProfile) LB_LightingProfile.fogDistance = fDistance;
					if(LB_LightingProfile) LB_LightingProfile.fogColor = fColor;
					if(LB_LightingProfile) LB_LightingProfile.fogDensity = fDensity;
					if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
				}
			}
			//-----Global--------------------------------------------------------------------
			if (vFog == CustomFog.Global)
			{
				Color fColorRef = fColor;
				float fogDensityRef = fDensity;

				fDensity = (float)EditorGUILayout.Slider("Density",fDensity,0,40f);
				fColor = (Color)EditorGUILayout.ColorField("Color",fColor);

				if(fColorRef != fColor || fogDensityRef != fDensity )
				{
					helper.Update_GlobalFog(cameraList, vFog,fDistance,fHeight,fheightDensity,fColor,fDensity);
					if(LB_LightingProfile) LB_LightingProfile.fogColor = fColor;
					if(LB_LightingProfile) LB_LightingProfile.fogDensity = fDensity;
					if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
				}
			}
			//-----Height--------------------------------------------------------------------
			if (vFog == CustomFog.Height)
			{
				float fDistanceRef = fDistance;
				float fHeightRef = fHeight;
				Color fColorRef = fColor;
				float fheightDensityRef = fheightDensity;

				fDistance = (float)EditorGUILayout.FloatField("Start Distance",fDistance);
				fHeight = (float)EditorGUILayout.FloatField("Height",fHeight);
				fheightDensity = (float)EditorGUILayout.Slider("Height Density",fheightDensity,0,1f);
				fColor = (Color)EditorGUILayout.ColorField("Color",fColor);

				if(fHeightRef != fHeight || fheightDensityRef != fheightDensity || fColorRef != fColor || fDistanceRef != fDistance)
				{
					helper.Update_GlobalFog(cameraList,vFog,fDistance,fHeight,fheightDensity,fColor,fDensity);
					if(LB_LightingProfile) LB_LightingProfile.fogHeight = fHeight;
					if(LB_LightingProfile) LB_LightingProfile.fogHeightDensity = fheightDensity;
					if(LB_LightingProfile) LB_LightingProfile.fogColor = fColor;
					if(LB_LightingProfile) LB_LightingProfile.fogDistance = fDistance;

					if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
				}
			}
			//-----Global Fog Type--------------------------------------------------------------------
			if(vfogRef != vFog)
			{

				helper.Update_GlobalFog(cameraList,vFog,fDistance,fHeight,fheightDensity,fColor,fDensity);

				//-------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.fogMode = vFog;
				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Depth of Field

			if (helpBox)
				EditorGUILayout.HelpBox ("Activate Depth Of Field for the camera", MessageType.Info);

			var dofTypeRef = dofType;
			var dofRangeRef = dofRange;
			var dofBlurRef = dofBlur;
			var falloffRef = falloff;
			var dofQualityRef = dofQuality;
			var visualizeRef = visualize;

			// Auto focus
			var autoFocusRef = autoFocus;
			var afRangeRef = afRange;
			var afBlurRef = afBlur;
			var afSpeedRef = afSpeed;
			var afUpdateRef = afUpdate;
			var afRayLengthRef = afRayLength;
			var afLayerRef = afLayer;

			dofType = (DOFType)EditorGUILayout.EnumPopup("Depth Of Field",dofType,GUILayout.Width(343));

			if (dofType == DOFType.On) 
			{
				dofQuality = (DOFQuality)EditorGUILayout.EnumPopup("Quality",dofQuality,GUILayout.Width(343));
				dofRange = (float)EditorGUILayout.Slider("Range",dofRange,0,100f);
				dofBlur = (float)EditorGUILayout.Slider("Blur",dofBlur,0,100f);
				falloff = (float)EditorGUILayout.Slider("Falloff",falloff,0,100f);
				visualize = (bool)EditorGUILayout.Toggle("Visualize",visualize);

				EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

				autoFocus = (AutoFocus)EditorGUILayout.EnumPopup("Auto Focus",autoFocus,GUILayout.Width(343));

				if(autoFocus == AutoFocus.On)
				{

					ScriptableObject targetL = this;
					SerializedObject soL = new SerializedObject(targetL);
					SerializedProperty stringsProperty = soL.FindProperty("afLayer");
					EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
					soL.ApplyModifiedProperties(); // Remember to apply modified properties

					afRange = (float)EditorGUILayout.Slider("Max Range",afRange,0,1000f);
					afBlur = (float)EditorGUILayout.Slider("Max Blur",afBlur,0,1000f);
					afSpeed = (float)EditorGUILayout.Slider("Speed",afSpeed,0,1000f);
					afUpdate = (float)EditorGUILayout.Slider("Raycst Update",afUpdate,0,1f);
					afRayLength = (float)EditorGUILayout.Slider("Ray Length",afRayLength,0,1000f);
				}

						if(afLayerRef != afLayer || afRangeRef != afRange || afBlurRef != afBlur
							|| afSpeedRef != afSpeed || afUpdateRef != afUpdate || afRayLengthRef != afRayLength
							|| autoFocusRef != autoFocus)
						{

							helper.Update_AutoFocus(cameraList,dofType,autoFocus,afLayer,afRange,afBlur,afSpeed,afUpdate,afRayLength);

							//----------------------------------------------------------------------
							if(LB_LightingProfile) LB_LightingProfile.afLayer = afLayer;
							if(LB_LightingProfile) LB_LightingProfile.afRange = afRange;
							if(LB_LightingProfile) LB_LightingProfile.afBlur = afBlur;
							if(LB_LightingProfile) LB_LightingProfile.afSpeed = afSpeed;
							if(LB_LightingProfile) LB_LightingProfile.afUpdate = afUpdate;
							if(LB_LightingProfile) LB_LightingProfile.afRayLength = afRayLength;
							if(LB_LightingProfile) LB_LightingProfile.autoFocus = autoFocus;

							if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
						}
			}

			if(dofTypeRef != dofType || dofRangeRef != dofRange || dofBlurRef != dofBlur
				|| dofQualityRef != dofQuality || falloffRef != falloff
				|| visualizeRef != visualize )
			{

				helper.Update_DOF(cameraList,dofType,dofQuality,dofBlur,dofRange,falloff,visualize);
				helper.Update_AutoFocus(cameraList,dofType,autoFocus,afLayer,afRange,afBlur,afSpeed,afUpdate,afRayLength);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.dofType = dofType;
				if(LB_LightingProfile) LB_LightingProfile.dofRange = dofRange;
				if(LB_LightingProfile) LB_LightingProfile.dofQuality = dofQuality;
				if(LB_LightingProfile) LB_LightingProfile.dofBlur = dofBlur;
				if(LB_LightingProfile) LB_LightingProfile.falloff = falloff;
				if(LB_LightingProfile) LB_LightingProfile.visualize = visualize;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

		}

		if (winMode == WindowMode.Part3) 
		{

			#region Color Grading

			if (helpBox)
				EditorGUILayout.HelpBox ("Color grading settings", MessageType.Info);

			var colorModeRef = colorMode;
			var exposureIntensityRef = exposureIntensity;
			var contrastValueRef = contrastValue;
			var tempRef = temp;
			var eyeKeyValueRef = eyeKeyValue;
			var gammaRef = gamma;
			var colorGammaRef = colorGamma;
			var colorLiftRef = colorLift;
			var saturationRef = saturation;
			var liftRef = lift;

			if(!webGL_Mobile)
				colorMode = (ColorMode)EditorGUILayout.EnumPopup("Mode",colorMode,GUILayout.Width(343));
			exposureIntensity = (float)EditorGUILayout.Slider("Exposure",exposureIntensity,0,3f);
			contrastValue = (float)EditorGUILayout.Slider("Contrast",contrastValue,0,1f);
			saturation = (float)EditorGUILayout.Slider("Saturation",saturation,-1f,0.3f);
			temp = (float)EditorGUILayout.Slider("Temperature",temp,0,100f);
			if(!webGL_Mobile)
				eyeKeyValue = (float)EditorGUILayout.Slider("Auto Exposure",eyeKeyValue,0,1f);
			colorGamma = (Color)EditorGUILayout.ColorField("Gamma Color",colorGamma);
			colorLift = (Color)EditorGUILayout.ColorField("Lift Color",colorLift);
			gamma = (float)EditorGUILayout.Slider("Gamma",gamma,-1f,1f);
			lift = (float)EditorGUILayout.Slider("Lift",lift,-1f,1f);

			if(exposureIntensityRef != exposureIntensity || contrastValueRef != contrastValue || tempRef != temp || eyeKeyValueRef != eyeKeyValue
				|| colorModeRef != colorMode || gammaRef != gamma || colorGammaRef != colorGamma || colorLiftRef != colorLift || saturationRef != saturation || liftRef != lift)
			{


				helper.Update_ColorGrading(colorMode,exposureIntensity,contrastValue,temp,eyeKeyValue,saturation,colorGamma,colorLift,gamma, lift);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.exposureIntensity = exposureIntensity;
				if(LB_LightingProfile) LB_LightingProfile.contrastValue = contrastValue;
				if(LB_LightingProfile) LB_LightingProfile.temp = temp;
				if(LB_LightingProfile) LB_LightingProfile.eyeKeyValue = eyeKeyValue;
				if(LB_LightingProfile) LB_LightingProfile.colorMode = colorMode;
				if(LB_LightingProfile) LB_LightingProfile.colorLift = colorLift;
				if(LB_LightingProfile) LB_LightingProfile.colorGamma = colorGamma;
				if(LB_LightingProfile) LB_LightingProfile.gamma = gamma;
				if(LB_LightingProfile) LB_LightingProfile.saturation = saturation;
				if(LB_LightingProfile) LB_LightingProfile.lift = lift;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Bloom

			if (helpBox)
				EditorGUILayout.HelpBox ("Activate Bloom for the camera", MessageType.Info);

			var bModeRef = bMode;
			var bIntensityRef = bIntensity;
			var bThreshouldRef = bThreshould;
			var bColorRef = bColor;

			bMode = (BloomMode)EditorGUILayout.EnumPopup("Bloom",bMode,GUILayout.Width(343));

			if(bMode == BloomMode.On)
			{
				bIntensity = (float)EditorGUILayout.Slider("Intensity",bIntensity,0,3f);
				bThreshould = (float)EditorGUILayout.Slider("Threshould",bThreshould,0,2f);
				bColor= (Color)EditorGUILayout.ColorField("Color",bColor);
			}

			if(bModeRef != bMode || bIntensityRef != bIntensity || bColorRef != bColor || bThreshouldRef != bThreshould || bIntensityRef != bIntensity)
			{


				if(bMode == BloomMode.On)
					helper.Update_Bloom(true,bIntensity,bThreshould , bColor);
				if(bMode == BloomMode.Off)
					helper.Update_Bloom(false,bIntensity,bThreshould , bColor);

				//----------------------------------------------------------------------

				if(LB_LightingProfile) LB_LightingProfile.bMode = bMode;
				if(LB_LightingProfile) LB_LightingProfile.bIntensity = bIntensity;
				if(LB_LightingProfile) LB_LightingProfile.bThreshould = bThreshould;
				if(LB_LightingProfile) LB_LightingProfile.bColor = bColor;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();
			#endregion

		}

		if (winMode == WindowMode.Finish)
		{

			#region Anti Aliasing

			if (helpBox)
				EditorGUILayout.HelpBox ("Activate Antialiasing for the camera", MessageType.Info);

			var aaModeRef = aaMode;

			aaMode = (AAMode)EditorGUILayout.EnumPopup("Anti Aliasing",aaMode,GUILayout.Width(343));

			if(aaModeRef != aaMode)
			{

				helper.Update_AA(cameraList,aaMode);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.aaMode = aaMode;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);


			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();


			#endregion

			#region AO
			if (helpBox)
				EditorGUILayout.HelpBox ("Activate AO for the camera", MessageType.Info);

			var aoModeRef = aoMode;
			var aoIntensityRef = aoIntensity;
			var ambientOnlyRef = ambientOnly;
			var aoTypeRef = aoType;
			var aoRadiusRef = aoRadius;
			var aoColorRef = aoColor;
			var aoQualityRef = aoQuality;

			aoMode = (AOMode)EditorGUILayout.EnumPopup("Ambient Occlusion",aoMode,GUILayout.Width(343));

			if(aoMode == AOMode.On)
			{
				aoType = (AOType)EditorGUILayout.EnumPopup("Type",aoType,GUILayout.Width(343));

				if(aoType == AOType.Modern)
				{
					aoIntensity = (float)EditorGUILayout.Slider("Intensity",aoIntensity,0,2f);
					aoColor	= (Color)EditorGUILayout.ColorField("Color",aoColor);
					ambientOnly = (bool)EditorGUILayout.Toggle("Ambient Only",ambientOnly);
				}
				if(aoType == AOType.Classic)
				{
					aoRadius = (float)EditorGUILayout.Slider("Radius",aoRadius,0,4.3f);
					aoIntensity = (float)EditorGUILayout.Slider("Intensity",aoIntensity,0,4f);
					aoColor	= (Color)EditorGUILayout.ColorField("Color",aoColor);
					aoQuality = (AmbientOcclusionQuality)EditorGUILayout.EnumPopup("Quality",aoQuality,GUILayout.Width(343));
					ambientOnly = (bool)EditorGUILayout.Toggle("Ambient Only",ambientOnly);
				}
			}

			if(aoModeRef != aoMode || aoIntensityRef != aoIntensity || ambientOnlyRef != ambientOnly
				|| aoTypeRef != aoType || aoRadiusRef != aoRadius || aoColorRef != aoColor || aoQualityRef != aoQuality)
			{

				if(aoMode == AOMode.On)
					helper.Update_AO(cameraList,true,aoType,aoRadius,aoIntensity,ambientOnly,aoColor, aoQuality);
				if(aoMode == AOMode.Off)
					helper.Update_AO(cameraList,false,aoType,aoRadius,aoIntensity,ambientOnly,aoColor, aoQuality);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.aoMode = aoMode;
				if(LB_LightingProfile) LB_LightingProfile.aoIntensity = aoIntensity;
				if(LB_LightingProfile) LB_LightingProfile.ambientOnly = ambientOnly;
				if(LB_LightingProfile) LB_LightingProfile.aoColor = aoColor;
				if(LB_LightingProfile) LB_LightingProfile.aoRadius = aoRadius;
				if(LB_LightingProfile) LB_LightingProfile.aoType = aoType;
				if(LB_LightingProfile) LB_LightingProfile.aoQuality = aoQuality;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Effects
			if (helpBox)
				EditorGUILayout.HelpBox ("Effects settings", MessageType.Info);

			var mbModeRef = mbMode;
			var vtModeRef = vtMode;
			var caModeRef = caMode;
			var ssrModeRef = ssrMode;
			var ssrBlurRef = ssrBlur;
			var interCountRef =  interCount;

			mbMode = (MBMode)EditorGUILayout.EnumPopup("Motion Blur",mbMode,GUILayout.Width(343));
			vtMode = (VTMode)EditorGUILayout.EnumPopup("Vignette",vtMode,GUILayout.Width(343));
			caMode = (CAMode)EditorGUILayout.EnumPopup("Chromatic Aberration",caMode,GUILayout.Width(343));
			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			ssrMode = (SSRMode)EditorGUILayout.EnumPopup("Screen Space Reflections",ssrMode,GUILayout.Width(343));

			if(ssrMode == SSRMode.On)
			{
				ssrBlur = (float)EditorGUILayout.Slider("Blur",ssrBlur,0,10f);
				interCount = (int)EditorGUILayout.Slider("Interation Count",interCount,256,1024);

			}

			if(mbModeRef != mbMode || vtModeRef != vtMode || caModeRef != caMode || ssrModeRef != ssrMode || ssrBlurRef != ssrBlur
				|| interCountRef != interCount)
			{


				// MotionBlur
				if (mbMode == MBMode.On)
					helper.Update_MotionBlur (true);
				else
					helper.Update_MotionBlur (false);

				// Vignette
				if(vtMode == VTMode.On)
					helper.Update_Vignette (true);
				else
					helper.Update_Vignette (false);

				// _ChromaticAberration
				if(caMode == CAMode.On)
					helper.Update_ChromaticAberration (true);
				else
					helper.Update_ChromaticAberration (false);

				// Screen Space Reflections
				helper.Update_SSR(cameraList, ssrMode, ssrBlur, interCount);

				//----------------------------------------------------------------------
				if(LB_LightingProfile) LB_LightingProfile.mbMode = mbMode;
				if(LB_LightingProfile) LB_LightingProfile.vtMode = vtMode;
				if(LB_LightingProfile) LB_LightingProfile.caMode = caMode;
				if(LB_LightingProfile) LB_LightingProfile.ssrMode = ssrMode;

				if(LB_LightingProfile) EditorUtility.SetDirty(LB_LightingProfile);
			}

			EditorGUILayout.Space (); GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});EditorGUILayout.Space ();

			#endregion

			#region Check for updates

			if (GUILayout.Button ("Check for updates"))  
			{
				
				EditorApplication.ExecuteMenuItem ("Assets/Lighting Box Updates");

			}



			#endregion

		}

		EditorGUILayout.EndScrollView();
	}
		#region Debug Lighting
		public void CheckColorSpace()
		{
			if (PlayerSettings.colorSpace == ColorSpace.Gamma) {
				if (GUILayout.Button ("Prefered color space is Linear, Current is Gamma", myFoldoutStyle))
					PlayerSettings.colorSpace = ColorSpace.Linear;
			}
		}

		public void CheckLightingMode()
		{
			Light[] lights = GameObject.FindObjectsOfType<Light> ();
			foreach (Light l in lights) 
			{
				// Check realtime state light types
				if (Lightmapping.realtimeGI == true) 
				{
					SerializedObject serialLightSource = new SerializedObject(l);
					SerializedProperty SerialProperty  = serialLightSource.FindProperty("m_Lightmapping");
					if (SerialProperty.intValue == 2) {

						if (GUILayout.Button (l.name + " : Change light type to realtime in realtime lighting mode", myFoldoutStyle)) {
							SerialProperty.intValue = 1;
							serialLightSource.ApplyModifiedProperties ();
						}
					}
				}

				// Check baked state light types
				if (Lightmapping.bakedGI == true) 
				{
					SerializedObject serialLightSource = new SerializedObject(l);
					SerializedProperty SerialProperty  = serialLightSource.FindProperty("m_Lightmapping");
					if (SerialProperty.intValue == 4)
					{
						if (GUILayout.Button (l.name + " : Change light type to Baked/Mixed in Baked lighting mode", myFoldoutStyle)) {
							SerialProperty.intValue = 2;
							serialLightSource.ApplyModifiedProperties ();
						}
					}
				}

				if (vLight != VolumetricLightType.Off) {

					if(vLight != VolumetricLightType.OnlyDirectional)
					{
						if (!l.GetComponent<VolumetricLight> ())
						{
							if (GUILayout.Button (l.name + " : Don't has VolumetricLight compoennt", myFoldoutStyle)) {
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
					}
					if(vLight == VolumetricLightType.OnlyDirectional)
					{
						if (l.type == LightType.Directional) {
							if (!l.GetComponent<VolumetricLight> ()) {
								if (GUILayout.Button (l.name + " : Don't has VolumetricLight compoennt", myFoldoutStyle)) {
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
						}
					}
				}
			}
			if (!sunLight)
			{
				if (GUILayout.Button ("Sunlight could not be found", myFoldoutStyle))
					EditorApplication.ExecuteMenuItem("GameObject/Light/Directional Light");
			}
		}

		void CheckSettings()
		{


			foreach (Camera c in cameraList) {
				if (c.allowMSAA) {
					if (GUILayout.Button ("Disable MSAA in camera component", myFoldoutStyle))
						c.allowMSAA = false;
				}
				if (!c.allowHDR) {
					if (GUILayout.Button ("Enable Allow HDR in camera component", myFoldoutStyle))
						c.allowHDR = true;
				}

				if(vLight != VolumetricLightType.Off)
				{
					if (!c.GetComponent<VolumetricLightRenderer> ()) {
						if (GUILayout.Button (c.name + ": VolumetricLightRenderer component is missing on camera", myFoldoutStyle))
							c.gameObject.AddComponent<VolumetricLightRenderer> ();
					}
				}
				if(vLight == VolumetricLightType.Off)
				{
					if (c.GetComponent<VolumetricLightRenderer> ()) {
						if (GUILayout.Button (c.name + ": VolumetricLightRenderer component is not necessary", myFoldoutStyle))
							c.gameObject.AddComponent<VolumetricLightRenderer> ();
					}
				}
			}

			if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ||
				EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS ||
				EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL) {
				GUILayout.Label ("Current build target is not compatible for next-gen effects");
				if (GUILayout.Button ("Switch to PC", myFoldoutStyle))
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone ,BuildTarget.StandaloneWindows64);
				if (GUILayout.Button ("Switch to PS4", myFoldoutStyle))
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.PS4 ,BuildTarget.PS4);
				if (GUILayout.Button ("Switch to Xbox One", myFoldoutStyle))
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.XboxOne ,BuildTarget.XboxOne);
				if (GUILayout.Button ("Switch to OSX Universal", myFoldoutStyle))
					EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone ,BuildTarget.StandaloneOSXUniversal);
			}
		}
		#endregion

		#region Update Settings
		void UpdateSettings()
		{
			// Camera 
			if(camMode == CameraMode.All)
				cameraList = GameObject.FindObjectsOfType<Camera>();

			if(camMode == CameraMode.Single)
			{
				cameraList = new Camera[1];
				cameraList[0] = Camera.main;
			}
			//-------------------------------------

			// Sun Light Update
			if (sunLight) {
				sunLight.color = sunColor;
				sunLight.intensity = sunIntensity;
				sunLight.bounceIntensity = indirectIntensity;
			} else {
				Update_Sun ();
			}

			if (sunFlare)
			{
				if(sunLight)
					sunLight.flare = sunFlare;
			}
			else
			{
				if(sunLight)
					sunLight.flare = null;
			}

			// Skybox
			helper.Update_SkyBox (skyBox);

			// Update Lighting Mode
			helper.Update_LightingMode(lightingMode);

			// Update Ambient
			helper.Update_Ambient(ambientLight,ambientColor);

			// Lights settings
			helper.Update_LightSettings (lightSettings);

			// Color Space
			helper.Update_ColorSpace(colorSpace);

			// Render Path
			helper.Update_RenderPath(renderPath,cameraList);

			// Volumetric Light
			helper.Update_VolumetricLight(cameraList,vLight,vLightLevel);

			// Sun Shaft
			helper.Update_SunShaft(cameraList,shaftMode, shaftQuality,shaftDistance,shaftBlur,shaftColor,sunLight.transform);

			// Shadows
			helper.Update_Shadows(psShadow);

			// Light Probes
			helper.Update_LightProbes(lightprobeMode);

			// Auto Mode
			helper.Update_AutoMode(autoMode);

			// Global Fog
			helper.Update_GlobalFog(cameraList,vFog,fDistance,fHeight,fheightDensity,fColor,fDensity);

		}
		#endregion

		#region Update Camera
		public void UpdateCamera()
		{

			if(camMode == CameraMode.All)
				cameraList = GameObject.FindObjectsOfType<Camera>();

			if(camMode == CameraMode.Single)
			{
				cameraList = new Camera[1];
				if (Camera.main)
					cameraList [0] = Camera.main;
				else
					cameraList [0] = GameObject.FindObjectOfType<Camera> ();
			}
		}
		#endregion

		#region On Load
		// load saved data based on project and scene name
		void OnLoad()
		{
			// Camera 
			UpdateCamera();

			//-------------------------------------


			if (!GameObject.Find ("LightingBox_Helper")) 
			{
				GameObject helperObject = new GameObject ("LightingBox_Helper");
				helperObject.AddComponent<LB_LightingBoxHelper> ();
				helper = helperObject.GetComponent<LB_LightingBoxHelper> ();
			}


			if (LB_LightingProfile) {
				lightingMode = LB_LightingProfile.lightingMode;
				if (LB_LightingProfile.skyBox)
					skyBox = LB_LightingProfile.skyBox;
				else
					skyBox = RenderSettings.skybox;
				sunFlare = LB_LightingProfile.sunFlare;
				ambientLight = LB_LightingProfile.ambientLight;
				renderPath = LB_LightingProfile.renderPath;
				lightSettings = LB_LightingProfile.lightSettings;
				sunColor = LB_LightingProfile.sunColor;

				// Color Space
				colorSpace = LB_LightingProfile.colorSpace;

				// Volumetric Light
				vLight = LB_LightingProfile.vLight;
				vLightLevel = LB_LightingProfile.vLightLevel;

				// Shadows
				psShadow = LB_LightingProfile.lightsShadow;

				// Fog
				vFog = LB_LightingProfile.fogMode;
				fDistance = LB_LightingProfile.fogDistance;
				fHeight = LB_LightingProfile.fogHeight;
				fheightDensity = LB_LightingProfile.fogHeightDensity;
				fColor = LB_LightingProfile.fogColor;
				fDensity = LB_LightingProfile.fogDensity;

				// DOF
				dofType = LB_LightingProfile.dofType;
				dofRange = LB_LightingProfile.dofRange;
				dofBlur = LB_LightingProfile.dofBlur;
				falloff = LB_LightingProfile.falloff;
				dofQuality = LB_LightingProfile.dofQuality;
				visualize = LB_LightingProfile.visualize;
				// Auto Focus
				autoFocus = LB_LightingProfile.autoFocus;
				afRange = LB_LightingProfile.afRange;
				afBlur = LB_LightingProfile.afBlur;
				afSpeed = LB_LightingProfile.afSpeed;
				afUpdate = LB_LightingProfile.afUpdate;
				afRayLength = LB_LightingProfile.afRayLength;
				afLayer = LB_LightingProfile.afLayer;

				// AA
				aaMode = LB_LightingProfile.aaMode;

				// AO
				aoMode = LB_LightingProfile.aoMode;
				aoIntensity = LB_LightingProfile.aoIntensity;
				aoColor = LB_LightingProfile.aoColor;
				ambientOnly = LB_LightingProfile.ambientOnly;
				aoRadius = LB_LightingProfile.aoRadius;
				aoType = LB_LightingProfile.aoType;
				aoQuality = LB_LightingProfile.aoQuality;

				// Bloom
				bIntensity = LB_LightingProfile.bIntensity;
				bColor = LB_LightingProfile.bColor;
				bMode = LB_LightingProfile.bMode;
				bThreshould = LB_LightingProfile.bThreshould;

				// Color Grading
				exposureIntensity = LB_LightingProfile.exposureIntensity;
				contrastValue = LB_LightingProfile.contrastValue;
				temp = LB_LightingProfile.temp;
				eyeKeyValue = LB_LightingProfile.eyeKeyValue;
				colorMode = LB_LightingProfile.colorMode;
			colorGamma = LB_LightingProfile.colorGamma;
			colorLift = LB_LightingProfile.colorLift;
				gamma = LB_LightingProfile.gamma;
				saturation = LB_LightingProfile.saturation;
				lift = LB_LightingProfile.lift;

				// Effects
				mbMode = LB_LightingProfile.mbMode;
				vtMode = LB_LightingProfile.vtMode;
				caMode = LB_LightingProfile.caMode;
				ssrMode = LB_LightingProfile.ssrMode;
				ssrBlur = LB_LightingProfile.ssrBlur;
				interCount = LB_LightingProfile.interCount;

				// Lightmap
				bakedResolution = LB_LightingProfile.bakedResolution;
				ambientColor = LB_LightingProfile.ambientColor;
				sunIntensity = LB_LightingProfile.sunIntensity;
				indirectIntensity = LB_LightingProfile.indirectIntensity;

				// Auto lightmap
				autoMode = LB_LightingProfile.automaticLightmap;

				// WebGL
				webGL_Mobile = LB_LightingProfile.webGL_Mobile;

				// Sun Shaft
				shaftMode = LB_LightingProfile.shaftMode;
				shaftDistance = LB_LightingProfile.shaftDistance;
				shaftBlur = LB_LightingProfile.shaftBlur;
				shaftColor = LB_LightingProfile.shaftColor;
				shaftQuality = LB_LightingProfile.shaftQuality;

				foreach (Camera c in cameraList) 
				{
					c.allowHDR = true;
					c.allowMSAA = false;
				}
				if (LB_LightingProfile.postProcessingProfile)
					postProcessingProfile = LB_LightingProfile.postProcessingProfile;
			}

			UpdatePostEffects ();

			UpdateSettings ();

			Update_Sun();

		}
		#endregion

		#region Update Post Effects Settings

		public void UpdatePostEffects()
		{
			UpdateCamera();

			if(!helper)
				helper = GameObject.Find("LightingBox_Helper").GetComponent<LB_LightingBoxHelper> ();

			if (!postProcessingProfile)
				return;

			helper.UpdateProfiles (cameraList, postProcessingProfile);

			// MotionBlur
			if (mbMode == MBMode.On)
				helper.Update_MotionBlur (true);
			else
				helper.Update_MotionBlur (false);

			// Vignette
			if(vtMode == VTMode.On)
				helper.Update_Vignette (true);
			else
				helper.Update_Vignette (false);

			// _ChromaticAberration
			if(caMode == CAMode.On)
				helper.Update_ChromaticAberration (true);
			else
				helper.Update_ChromaticAberration (false);

			// Bloom
			if (bMode == BloomMode.On)
				helper.Update_Bloom(true,bIntensity,bThreshould,bColor);
			else
				helper.Update_Bloom(false,bIntensity,bThreshould,bColor);

			// Depth of Field
			helper.Update_DOF(cameraList,dofType,dofQuality,dofBlur,dofRange,falloff,visualize);
			helper.Update_AutoFocus(cameraList,dofType,autoFocus,afLayer,afRange,afBlur,afSpeed,afUpdate,afRayLength);

			// AO
			if (aoMode == AOMode.On)
				helper.Update_AO(cameraList,true,aoType,aoRadius,aoIntensity,ambientOnly,aoColor, aoQuality);
			else
				helper.Update_AO(cameraList,false,aoType,aoRadius,aoIntensity,ambientOnly,aoColor, aoQuality);


			// Color Grading
		helper.Update_ColorGrading(colorMode,exposureIntensity,contrastValue,temp,eyeKeyValue,saturation,colorGamma,colorLift,gamma, lift);

			////-----------------------------------------------------------------------------
			/// 
			// Screen Space Reflections
			helper.Update_SSR(cameraList, ssrMode, ssrBlur, interCount);

		}
		#endregion

		#region Scene Delegate

		string currentScene;
		void SceneChanging ()
		{
			if (currentScene != EditorSceneManager.GetActiveScene().name)
			{
				if (System.String.IsNullOrEmpty (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name)))
					LB_LightingProfile = Resources.Load ("DefaultSettings")as LB_LightingProfile;
				else 
					LB_LightingProfile = (LB_LightingProfile)AssetDatabase.LoadAssetAtPath (EditorPrefs.GetString (EditorSceneManager.GetActiveScene ().name), typeof(LB_LightingProfile));

				OnLoad ();
				currentScene = EditorSceneManager.GetActiveScene().name;
			}
		}
		#endregion

		#region Sun Light
		public void Update_Sun()
		{
			if (!RenderSettings.sun) 
			{
				Light[] lights = GameObject.FindObjectsOfType<Light> ();
				foreach (Light l in lights) {
					if (l.type == LightType.Directional) 
					{
						sunLight = l;

						if (sunColor != Color.clear)
							sunColor = l.color;
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

		#endregion

		#region On Download Completed
		bool downloadFinished = true;
		void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error == null)
				downloadFinished = true;
			else 
				Debug.Log (e.Error);
		}
		#endregion

	}
#endif