using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    bool _canLook = true;

    [HideInInspector]
    public Transform Camera => _camera;
    [SerializeField] Transform _camera;

    [Header("Inputs")]
    [SerializeField] InputActionReference Look;

    public float CameraSensitivityX;
    public float CameraSensitivityY;

    [SerializeField] float MinAngleRotation = -90f;
    [SerializeField] float MaxAngleRotation = 90f;

    [SerializeField] Transform _orientation;
    public float _xRot;
    public float _yRot;

    void Start()
    {
        SetCameraRotation(Quaternion.identity);
        ToggleCursorState(false);
    }

    private void Update()
    {
        if (_canLook)
            rotateCamera();
    }

    private void rotateCamera()
    {
        float mouseX = Look.action.ReadValue<Vector2>().x * Time.deltaTime * CameraSensitivityX;
        float mouseY = Look.action.ReadValue<Vector2>().y * Time.deltaTime * CameraSensitivityY;

        _yRot += mouseX;
        _xRot -= mouseY;
        _xRot = Mathf.Clamp(_xRot, MinAngleRotation, MaxAngleRotation);

        _camera.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        _orientation.rotation = Quaternion.Euler(0f, _yRot, 0f);
    }

    public void ToggleCursorState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    public void ToggleCanLook(bool state)
    {
        _canLook = state;
    }

    public void SetCameraRotation(Vector3 angle)
    {
        _camera.rotation = Quaternion.Euler(angle);
        _orientation.rotation = Quaternion.Euler(0f, angle.y, 0f);
    }

    public void SetCameraRotation(Quaternion angle)
    {
        _camera.rotation = angle;
        _orientation.rotation = Quaternion.Euler(0f, angle.eulerAngles.y, 0f);

        _yRot = angle.eulerAngles.y;
        _xRot = angle.eulerAngles.x;
    }
}
