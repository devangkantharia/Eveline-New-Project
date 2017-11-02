using UnityEngine;
public class SubmergedEffect : MonoBehaviour 
{
	public GameObject waterBody;
    public Color underWaterColor;
	public float underWaterVisiblity;
	public bool aboveWaterFogMode;
	public Color aboveWaterColor;
	public float aboveWaterVisiblity;
	public GameObject WaterParticles;
	private GameObject Player;
	public Projector Caustics;
	public bool checkedIfAboveWater;
	public float waterHeight;
    public Player player;

	void Start () 
	{
	    ParticleSystem.EmissionModule waterParticles_emission; // Locate and cache the underwater particles effect and enable it
        waterParticles_emission = WaterParticles.GetComponent<ParticleSystem>().emission;
        waterParticles_emission.enabled = true;

		underWaterColor = new Color(32.0f/255.0F,116.0f/255.0F,166.0f/255.0F,255.0f/255.0F); // Set the color for underwater fog
		Player = GameObject.FindGameObjectWithTag("Player"); // Cache the player
		Caustics.enabled = false; // Initially turn off the caustics effect as we start above water
		checkedIfAboveWater = false;
		underWaterVisiblity = 0.04f; // Set the Underwater Visibility - can be adjusted publicly
        // Cache the audiosources for underwater, splash in and splash out of water
		Camera.main.nearClipPlane = 0.1f;
		waterHeight = waterBody.transform.position.y; // This is critical! It is the height of the water plane to determine we are underwater or not
		AssignAboveWaterSettings (); // Initially set above water settings
        player = FindObjectOfType<Player>();
	}
	// Update is called once per frame
	void Update () 
	{
        // the checkedifAboveWater stops it forcing it over and over every frame if we already know where we are
        // If tghe player is above water and we haven't confirmed this yet, then set settings for above water and confirm
		if (transform.position.y >= waterHeight && checkedIfAboveWater == false && player.isSubmerged == false) 
		{
            checkedIfAboveWater = true;
			ApplyAboveWaterSettings ();
			ToggleFlares (true);
		}
        // If we are under water and we haven't confirmed this yet, then set for under water and confirm
		if (transform.position.y < waterHeight && checkedIfAboveWater == true && player.isSubmerged == true) 
		{
			checkedIfAboveWater = false;
			ApplyUnderWaterSettings ();
			ToggleFlares (false);
		}
	}
    // Initially assign current world fog ready for reuse later in above water 
	void AssignAboveWaterSettings () 
	{
		aboveWaterFogMode = RenderSettings.fog;
		aboveWaterColor = RenderSettings.fogColor;
		aboveWaterVisiblity = RenderSettings.fogDensity;
	}
    // Apply Abovewater default settings - enabling the above water view and effects
    void ApplyAboveWaterSettings () 
	{
		if(WaterParticles.GetComponent<ParticleSystem>().isPlaying)
		{
			WaterParticles.GetComponent<ParticleSystem>().Stop ();
			WaterParticles.GetComponent<ParticleSystem>().Clear ();
		}
		RenderSettings.fog = false;
        Caustics.enabled = false;
	}

    // Apply Underwater default settings - enabling the under water view and effects
    void ApplyUnderWaterSettings () 
	{
		RenderSettings.fog =true;
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underWaterVisiblity;
        Caustics.enabled = true;
		if(!WaterParticles.GetComponent<ParticleSystem>().isPlaying)
		{
			WaterParticles.GetComponent<ParticleSystem>().Play ();
		}
	}
    // Toggle flares on or off depending on whether we are underwater or not
	void ToggleFlares (bool state) 
	{
		LensFlare[] lensFlares = FindObjectsOfType(typeof(LensFlare)) as LensFlare[];
		foreach (LensFlare currentFlare in lensFlares) 
		{
			currentFlare.enabled = state;
		}
	}
}
