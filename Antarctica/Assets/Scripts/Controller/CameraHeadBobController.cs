using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadBobController : MonoBehaviour
{
    [SerializeField] bool HeadBobbingEnable;

    [SerializeField, Range(0, 0.1f)] float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] float _frequency = 10f;
    [SerializeField] float _sprintMultiplier = 2f;
    [SerializeField] Transform _cameraTarget;
    [SerializeField] Transform _cameraHolder;

    float _toggleSpeed = 3.0f;
    Vector3 _startPos;

    private void Start()
    {
        _startPos = _cameraTarget.localPosition;
    }

    public void SetHeadBobMovement(float speed, bool isSprinting)
    {
        if (!HeadBobbingEnable || speed < _toggleSpeed)
            return;

        playMotion(footStepMotion(isSprinting ? _sprintMultiplier : 1));
        ResetPosition();
        //_cameraHolder.LookAt(focusTarget());
    }

    void playMotion(Vector3 motion)
    {
        _cameraTarget.localPosition += motion;
    }

    void ResetPosition()
    {
        if (_cameraTarget.localPosition == _startPos)
            return;

        _cameraTarget.localPosition = Vector3.Lerp(_cameraTarget.localPosition, _startPos, 1 * Time.deltaTime);
    }

    Vector3 footStepMotion(float multipler = 1f)
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * _frequency * multipler) * _amplitude * multipler;
        pos.x = Mathf.Cos(Time.time * (_frequency / 2) * multipler) * (_amplitude * 2 * multipler);
        return pos;
    }

    Vector3 focusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.position.y, transform.position.z);
        pos += _cameraHolder.forward * 15f;
        return pos;
    }
}
