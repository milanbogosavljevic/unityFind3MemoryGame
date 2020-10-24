﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonClickHandler : MonoBehaviour
{
    public int levelNumber;
    private Camera _camera;
    private float _cameraX;

    void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseUpAsButton()
    {
        float oldX = PlayerPrefs.GetFloat("CameraPosition");
        _cameraX = _camera.transform.position.x;
        PlayerPrefs.SetFloat("CameraPosition", _cameraX);
        if (oldX == _cameraX)
        {
            SceneManager.LoadScene(levelNumber+3);
        }
    }

    /*private void OnMouseDown()
    {
        _cameraX = _camera.transform.position.x;
        PlayerPrefs.SetFloat("CameraPosition", _cameraX);
        SceneManager.LoadScene(levelNumber+3);
    }*/
}
