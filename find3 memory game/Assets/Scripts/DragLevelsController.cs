using UnityEngine;

public class DragLevelsController : MonoBehaviour
{
    private Camera _camera;
    private float _scrollSpeed = -0.01f;
    private Vector3 _transformPos;
    void Start()
    {
        _camera = GetComponent<Camera>();
        if (PlayerPrefs.HasKey("CameraPosition"))
        {
            _transformPos = transform.position;
            float xPosition = PlayerPrefs.GetFloat("CameraPosition");
            Vector3 newPosition = new Vector3(xPosition, _transformPos.y, _transformPos.z);
            _camera.transform.position = newPosition;
        }
    }
    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            _transformPos = transform.position;
            float xPosition = _transformPos.x + (touchDeltaPosition.x * _scrollSpeed);
            if (xPosition > 0 && xPosition < 11.32f)
            {
               Vector3 newPosition = new Vector3(xPosition, _transformPos.y, _transformPos.z);
               _camera.transform.position = newPosition;
            }
        }
    }
}
