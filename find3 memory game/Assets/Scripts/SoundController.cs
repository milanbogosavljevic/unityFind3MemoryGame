using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource selectCardSound;
    [SerializeField] private AudioSource levelPassedSound;

    private bool _musicIsOn;
    private bool _soundIsOn;

    private void Awake()
    {
        _musicIsOn = PlayerPrefs.GetString("MusicPlay") == "on";
        _soundIsOn = PlayerPrefs.GetString("SoundPlay") == "on";
        
        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundController");

        Debug.Log(objs.Length);
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (_musicIsOn)
        {
            Debug.Log("Play music");
            backgroundMusic.Play();
        }
    }

    public void SetSoundOn(bool on)
    {
        _soundIsOn = on;
    }
    
    public void SetMusicOn(bool on)
    {
        _musicIsOn = on;
        if (on)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }

    public void PlaySelectCardSound()
    {
        if (_soundIsOn)
        {
            selectCardSound.Play();
        }
    }

    public void PlayLevelPassedSound()
    {
        if (_soundIsOn)
        {
            levelPassedSound.Play();
        }
    }
}
