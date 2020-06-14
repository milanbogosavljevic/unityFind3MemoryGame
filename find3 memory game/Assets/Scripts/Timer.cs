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

    private string _secondsText;
    private string _minutesText;

    private bool _isActive;

    void Awake()
    {
        _isActive = false;
    }

    void Start()
    {
        _timer = 0f;
        _seconds = 0;
        _minutes = 0;
        _secondsText = "00";
        _minutesText = "00";
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
