using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCotroller : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minXAngle;
    [SerializeField] private float maxXAngle;
    [SerializeField] private float minYAngle;
    [SerializeField] private float maxYAngle;
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;

    [SerializeField] private Transform player;

    private MainSystem_actions _inputSystemActions;
    private float _rotationX = 0f;
    private float _rotationY = 0f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _inputSystemActions = new MainSystem_actions();
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
    }

    private void Update()
    {
        player.rotation = Quaternion.Euler(0, _rotationX, 0);

        RotateCamera();
        ZoomCamera();
    }

    private void RotateCamera()
    {
        Vector2 lookInput = _inputSystemActions.FindAction("Look").ReadValue<Vector2>();

        _rotationX += lookInput.x * rotateSpeed * Time.deltaTime;
        _rotationY -= lookInput.y * rotateSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, minXAngle, maxXAngle);
        _rotationY = Mathf.Clamp(_rotationY, minYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(_rotationY, _rotationX, 0f);
    }

    private void ZoomCamera()
    {
        float zoomInput = _inputSystemActions.FindAction("Zoom").ReadValue<float>();
        float newFOV = _camera.fieldOfView + zoomInput * zoomSpeed;
        newFOV = Mathf.Clamp(newFOV, minZoom, maxZoom);
        _camera.fieldOfView = newFOV;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
    }
}
