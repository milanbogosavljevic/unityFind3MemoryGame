using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButtonsController : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;

    void Start()
    {
        _setButtonsBestTime();
    }

    private void _setButtonsBestTime()
    {
        print("levels " + levelButtons.Count);
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int levelNumber = i + 1;
            string minutesKey = "level" + levelNumber + "Minutes";
            string secondsKey = "level" + levelNumber + "Seconds";
            string time = "00:00";
            if (PlayerPrefs.HasKey(minutesKey))
            {
                int minutes = PlayerPrefs.GetInt(minutesKey);
                int seconds = PlayerPrefs.GetInt(secondsKey);
                string minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
                string secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
                time = minutesText + ":" + secondsText;
                print(levelNumber);
                print(time);
            }
            levelButtons[i].GetComponentInChildren<Text>().text = time;
        }
    }
    
    public void LoadLevel(int levelNum)
    {
        int levelIndex = levelNum + 3;
        SceneManager.LoadScene(levelIndex);
    }

    public void BackToWelcomeScreen()
    {
        SceneManager.LoadScene(0);
    }
}
