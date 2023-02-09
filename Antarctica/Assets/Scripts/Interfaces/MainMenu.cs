using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PressPlay()
    {
        //Tempo
        SceneManager.LoadScene("Antarctica");
    }

    public void PressSettings()
    {

    }

    public void PressQuit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
