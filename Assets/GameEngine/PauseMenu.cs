using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool isActive = false;
    public GameObject mainUI;
    public GameObject optionsUI;
    public bool lockCursor = true;

    public bl_SceneLoader loading;

    //Controls
    [Header("Gameplay")]
    public static float sensitivityX = 2.0f;
    public static float sensitivityY = 2.0f;

    //VideoSettings
    [Header("Video Settings")]
    public Transform resolutionPanel;
    public GameObject ResolutionButtons;
    [Space(5)]
    public bool ShowFramesPerSecond = true;
    public Text FPSFrames = null;
    public float updateInterval = 0.5f;
    private float accum = 0;
    private int frames = 0;
    private float timeleft;

    //AudioSettings


    void Awake()
    {
        if (lockCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (mainUI != null)
        {
            mainUI.SetActive(false);
        }
        if(optionsUI != null)
        {
            optionsUI.SetActive(false);
        }
        PostResolutions();
        if (FPSFrames != null) { FPSFrames.gameObject.SetActive(ShowFramesPerSecond); }
        timeleft = updateInterval;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionsUI.activeSelf)
            {
                OptionsMenu();
            } else
            {
                pauseMenuActive();
            }
        }

        if (ShowFramesPerSecond && FPSFrames != null)
        {
            FramesPerSecond();
        }
    }

    public void pauseMenuActive()
    {
        if (mainUI.activeSelf)
        {
            Time.timeScale = 1;
            if (lockCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            PauseMenu.isActive = false;
            mainUI.SetActive(false);
        }
        else
        {
            PauseMenu.isActive = true;
            Time.timeScale = 0;
            if(lockCursor)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            mainUI.SetActive(true);
        }
    }

    public void OptionsMenu()
    {
        if (optionsUI.activeSelf)
        {
            optionsUI.SetActive(false);
        } else
        {
            optionsUI.SetActive(true);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToDesktop(string mainMenuSceneName)
    {
        Application.LoadLevel(mainMenuSceneName);
    }

    public void GoToScene(string nomeScena)
    {
        loading.LoadLevel(nomeScena);
    }

    //Update Sensitivity on X Axis
    public void SentitivityX(int sX)
    {
        sensitivityX = sX;
    }

    //Update Sensitivity on Y Axis
    public void SentitivityY(int sY)
    {
        sensitivityY = sY;
    }

    //Calculate and print FPS
    void FramesPerSecond()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft == 0.0)
        {
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            FPSFrames.text = format;

            if (fps < 30)
            {
                FPSFrames.color = Color.yellow;
            } else
            {
                if(fps < 10)
                {
                    FPSFrames.color = Color.red;
                } else
                {
                    FPSFrames.color = Color.green;
                }
            }

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }

    public void ChangeFPSFrames(bool b)
    {
        ShowFramesPerSecond = b;
        FPSFrames.gameObject.SetActive(b);
    }

    public void ChangeResolution(int r)
    {
        Screen.SetResolution(Screen.resolutions[r].width, Screen.resolutions[r].height, true);
    }

    public void ChangeQuality(int q)
    {
        QualitySettings.SetQualityLevel(q, true);
    }

    public void UpdateVolume(float v)
    {
        AudioListener.volume = v;
    }

    public void AntiAliasing(int a)
    {
        QualitySettings.antiAliasing = a;
    }

    public void TextureQuality(int tq)
    {
        QualitySettings.masterTextureLimit = tq;
    }

    public void UpdateAnisotropic(int a)
    {
        switch(a)
        {
            case 0:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                break;
            case 1:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                break;
            case 2:
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
                break;
        }
    }

    public void VSync(int vs)
    {
        QualitySettings.vSyncCount = vs;
    }

    public void ShadowCascades(int s)
    {
        QualitySettings.shadowCascades = s;
    }

    public void BlendWeight(int bw)
    {
        switch(bw)
        {
            case 0:
                QualitySettings.blendWeights = BlendWeights.OneBone;
                break;
            case 1:
                QualitySettings.blendWeights = BlendWeights.TwoBones;
                break;
            case 2:
                QualitySettings.blendWeights = BlendWeights.FourBones;
                break;
            
        }
    }

    public void SoftVegetation(bool b)
    {
        QualitySettings.softVegetation = b;
    }

    void PostResolutions()
    {
        if (resolutionPanel == null)
            return;
        if (ResolutionButtons == null)
            return;

        int n = -1;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            GameObject b = Instantiate(ResolutionButtons) as GameObject;
            b.GetComponentInChildren<Text>().text = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;
            b.transform.SetParent(resolutionPanel, false);
            b.GetComponent<Button>().onClick.AddListener(() => { ChangeResolution(n); });
            n++;
        }


    }
}
