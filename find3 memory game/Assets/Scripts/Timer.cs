using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private int _seconds;
    private int _minutes;
    private float _timer;

    private int _goldTime;
    private int _silverTime;
    private int _totalSeconds;

    private string _secondsText;
    private string _minutesText;

    private bool _isActive;

    private Color _goldColor;
    private Color _silverColor;
    private Color _bronzeColor;

    void Awake()
    {
        _isActive = false;
    }

    void Start()
    {
        _timer = 0f;
        _seconds = 0;
        _minutes = 0;
        _totalSeconds = 0;
        _secondsText = "00";
        _minutesText = "00";

        _goldTime = PlayerPrefs.GetInt("goldTime");
        _silverTime = PlayerPrefs.GetInt("silverTime");
        
        _goldColor = new Color32(255, 215, 0, 255);
        _silverColor = new Color32(192,192,192, 255);
        _bronzeColor = new Color32(205, 127, 50, 255);
    }

    void Update()
    {
        if (_isActive)
        {
            _updateTime();
        }
    }

    private void _updateTime()
    {
        _timer += Time.deltaTime;
        _seconds = Mathf.FloorToInt(_timer);
        if (_seconds >= 60)
        {
            _timer = 0f;
            _seconds = 0;
            _minutes++;
        }
        _secondsText = _seconds <= 9 ? "0" + _seconds.ToString() : _seconds.ToString();
        _minutesText = _minutes <= 9 ? "0" + _minutes.ToString() : _minutes.ToString();
        timeText.text = _minutesText + ":" + _secondsText;

        _totalSeconds = (_minutes * 60) + _seconds;

        if (_totalSeconds <= _goldTime)
        {
            if (timeText.color != _goldColor)
            {
                timeText.color = _goldColor;
            }
        }
        else
        {
            if (_totalSeconds <= _silverTime)
            {
                if (timeText.color != _silverColor)
                {
                    timeText.color = _silverColor;
                }
            }
            else
            {
                if (timeText.color != _bronzeColor)
                {
                    timeText.color = _bronzeColor;
                }
            }
        }
    }

    public void ActivateTimer(bool activate)
    {
        _isActive = activate;
    }

    public int[] GetTime()
    {
        int[ ] levelTime = { _minutes, _seconds };
        return levelTime;
    }
}
