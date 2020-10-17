using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    private float _targetAspect = 16f / 9f;
    private Camera _camera;
    private float _scrollSpeed = -0.2f;
    private Vector3 _transformPos;
    void Start()
    {
        _camera = GetComponent<Camera>();

        float currentAspect = (float)Screen.height / Screen.width;
        float difference;

        if (currentAspect != _targetAspect)
        {
            if (currentAspect > _targetAspect)
            {
                difference = currentAspect - _targetAspect;
                _camera.orthographicSize += (difference + 0.3f);
            }
            else
            {
                difference = _targetAspect - currentAspect;
                _camera.orthographicSize -= (difference + 0.3f);
            }
        }
    }
}
