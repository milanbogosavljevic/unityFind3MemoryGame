using UnityEngine;

public class DragLevelsController : MonoBehaviour
{
    private Camera _camera;
    private float _scrollSpeed = -0.2f;
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
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _transformPos = transform.position;
            float xPosition = _transformPos.x + (Input.GetAxis("Mouse X") * _scrollSpeed);
            if (xPosition > 0 && xPosition < 5.22f)
            {
                Vector3 newPosition = new Vector3(xPosition, _transformPos.y, _transformPos.z);
                _camera.transform.position = newPosition;
            }
        }
    }
}
