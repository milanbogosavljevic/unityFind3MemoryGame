using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsButtonsController : MonoBehaviour
{
    
    [SerializeField]
    private Toggle soundToggle; 
    
    [SerializeField]
    private Toggle musicToggle;
    void Start()
    {
        CheckIfMusicIsOn();
        CheckIfSoundIsOn();
    }

    private void CheckIfMusicIsOn()
    {
        if (PlayerPrefs.HasKey("MusicPlay") == false)
        {
            PlayerPrefs.SetString("MusicPlay", "on");
        }
        else
        {
            musicToggle.isOn = PlayerPrefs.GetString("MusicPlay") == "on";
        }
    }

    private void CheckIfSoundIsOn()
    {
        if (PlayerPrefs.HasKey("SoundPlay") == false)
        {
            PlayerPrefs.SetString("SoundPlay", "on");
        }
        else
        {
            soundToggle.isOn = PlayerPrefs.GetString("SoundPlay") == "on";
        }
    }

    public void BackToWelcomeScreen()
    {
        SceneManager.LoadScene(0);
    }
    
    
    public void SetSoundInGame()
    {
        string soundPlay = soundToggle.isOn ? "on" : "off";
        PlayerPrefs.SetString("SoundPlay", soundPlay);
    }

    public void SetMusicInGame()
    {
        string musicPlay = musicToggle.isOn ? "on" : "off";
        PlayerPrefs.SetString("MusicPlay", musicPlay);
    }

    public bool IsMusicOn()
    {
        return musicToggle.isOn;
    }
    
    public bool IsSoundOn()
    {
        return soundToggle.isOn;
    }
}
