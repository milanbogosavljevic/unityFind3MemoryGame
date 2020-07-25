using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{
    public void BackToLevelSelection()
    {
        SceneManager.LoadScene(1);
    }
}
