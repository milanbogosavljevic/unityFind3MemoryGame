using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonClickHandler : MonoBehaviour
{ 
    public void BackToWelcomeScreen()
    {
        Camera _camera = Camera.main;
        float _cameraX = _camera.transform.position.x;
        PlayerPrefs.SetFloat("CameraPosition", _cameraX);
        SceneManager.LoadScene(0);
    }
}
