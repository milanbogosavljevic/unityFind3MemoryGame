using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonsController : MonoBehaviour
{
    public void BackToWelcomeScreen()
    {
        SceneManager.LoadScene(0);
    }
    
    public void BackToLevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(3);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
