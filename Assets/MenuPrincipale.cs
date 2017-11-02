using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipale : MonoBehaviour {
    public bl_SceneLoader loadScreen;
    public bl_AllOptionsPro settings;

    public void NewGame(string firstLevel)
    {
        loadScreen.LoadLevel(firstLevel);
    }

    public void Options()
    {
        settings.ShowMenu();
    }  

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

