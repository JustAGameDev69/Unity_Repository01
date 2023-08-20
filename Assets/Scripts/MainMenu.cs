using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        //Application.Quit();           //Only enable when already built the game, not work in editor

        UnityEditor.EditorApplication.isPlaying = false;
    }

}
