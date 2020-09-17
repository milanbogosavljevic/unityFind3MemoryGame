using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLevelsController : MonoBehaviour
{
    private RectTransform _rect;
    private float _scrollSpeed;
    private Vector3 _transformPos;
    private const float MaxYPosition = 1400f;
    private const float MinYPosition = 100f;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _scrollSpeed = 20f;
        
        print(Screen.safeArea);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _transformPos = transform.position;
            float yPosition = _transformPos.y + (Input.GetAxis("Mouse Y") * _scrollSpeed);
            if (yPosition > MaxYPosition || yPosition < MinYPosition)
            {
                return;
            }
            //print(yPosition);
            Vector3 newPosition = new Vector3(_transformPos.x, yPosition, _transformPos.z);
            _rect.position = newPosition;
        }
    }
}
