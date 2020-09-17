using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    private float _targetAspect = 16f / 9f;
    private Camera _camera;
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
                _camera.orthographicSize += difference;
            }
            else
            {
                difference = _targetAspect - currentAspect;
                _camera.orthographicSize -= difference;
            }
        }
    }

}
