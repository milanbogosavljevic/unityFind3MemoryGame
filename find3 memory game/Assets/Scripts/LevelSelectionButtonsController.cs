using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButtonsController : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;
    [SerializeField] private Sprite goldCard;
    [SerializeField] private Sprite silverCard;
    [SerializeField] private Sprite bronzeCard;

    [SerializeField] private List<int> goldTimes;
    [SerializeField] private List<int> silverTimes;

    void Start()
    {
        _setButtonsBestTime();
    }

    private void _setButtonsBestTime()
    {
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
                int totalSeconds = (minutes * 60) + seconds;
                SetButtonImage(i, totalSeconds);
            }
            levelButtons[i].GetComponentInChildren<Text>().text = time;
        }
    }

    private void SetButtonImage(int buttonIndex, int seconds)
    {
        if (seconds <= silverTimes[buttonIndex])
        {
            levelButtons[buttonIndex].image.sprite = seconds <= goldTimes[buttonIndex] ? goldCard : silverCard;
        }
        else
        {
            levelButtons[buttonIndex].image.sprite = bronzeCard;
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
