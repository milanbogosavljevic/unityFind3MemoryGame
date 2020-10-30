using System;
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
        int goldTime = GameObject.Find("Levels").GetComponent<LevelButtonsController>().GetGoldTime(levelNumber - 1);
        int silverTime = GameObject.Find("Levels").GetComponent<LevelButtonsController>().GetSilverTime(levelNumber - 1);
        PlayerPrefs.SetInt("goldTime", goldTime);
        PlayerPrefs.SetInt("silverTime", silverTime);
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
