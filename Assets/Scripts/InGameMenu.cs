using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject SettingMenu;

    public void ShowSettingMenu()
    {
        SettingMenu.SetActive(true);
    }
    public void HideSettingMenu()
    {
        SettingMenu.SetActive(false);
    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
