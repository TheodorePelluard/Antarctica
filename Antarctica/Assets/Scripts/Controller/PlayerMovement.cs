using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    bool _canMove = true;

    [Header("Inputs")]
    [SerializeField] InputActionReference Move;
    [SerializeField] InputActionReference Sprint;

    float _horizontalInput;
    float _verticalInput;
    Vector3 _movementDirection;
    float _movementSpeed;

    [Header("Speed Parameters")]
    [SerializeField] float WalkSpeed = 7f;
    [SerializeField] float SprintSpeed = 12f;
    [SerializeField] float _groundDrag;

    [Header("Slopes Parameters")]
    [SerializeField] float _maxSlopeAngle = 35f;
    [SerializeField] float _slopeDownForce = 80f;
    RaycastHit _slopeHit;

    [Header("References")]
    [SerializeField] Transform _orientation;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _playerHeight;
    [SerializeField] CameraHeadBobController _headBobController;

    void Start()
    {
        _rb.freezeRotation = true;
    }

    void Update()
    {
        if (_canMove)
        {
            myInput();
            speedControl();

            if (onGround())
                _rb.drag = _groundDrag;
            else
                _rb.drag = 0f;

            _headBobController.SetHeadBobMovement(getFlatVelocity().magnitude, isSprinting());
        }
    }

    void FixedUpdate()
    {
        if(_canMove)
            movePlayer();
    }

    void myInput()
    {
        _horizontalInput = Move.action.ReadValue<Vector2>().x;
        _verticalInput = Move.action.ReadValue<Vector2>().y;
  
        if (isSprinting())
            _movementSpeed = SprintSpeed;
        else
            _movementSpeed = WalkSpeed;
    }

    void movePlayer()
    {
        _movementDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        bool onSlope = this.onSlope();

        if (onSlope)
        {
            _rb.AddForce(getSlopeMoveDirection() * _movementSpeed * 20f);

            if (_rb.velocity.y > 0)
                _rb.AddForce(Vector3.down * _slopeDownForce, ForceMode.Force);
        }
        else
            _rb.AddForce(_movementDirection.normalized * _movementSpeed * 10f, ForceMode.Force);

        _rb.useGravity = !onSlope;
    }

    void speedControl()
    {
        if (onSlope())
        {
            if (_rb.velocity.magnitude > _movementSpeed)
                _rb.velocity = _rb.velocity.normalized * _movementSpeed;
        }
        else
        {
            Vector3 flatVelocity = getFlatVelocity();

            if (flatVelocity.magnitude > _movementSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
                _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    public float GetLateralMovement()
    {
        return Move.action.ReadValue<Vector2>().x;
    }

    public void ToggleCanMove(bool state)
    {
        _canMove = state;
    }

    Vector3 getFlatVelocity()
    {
       return new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
    }

    bool onSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    bool onGround()
    {
        if (Physics.Raycast(transform.position + new Vector3(0f, _playerHeight * 0.5f,0f), Vector3.down, _playerHeight * 0.5f + 0.5f))
        {
            return true;
        }
        return false;
    }

    Vector3 getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_movementDirection, _slopeHit.normal).normalized;
    }

    bool isSprinting()
    {
        return Sprint.action.ReadValue<float>() > 0;
    }

    private void OnDrawGizmos()
    {
        if(onSlope())
            Gizmos.DrawLine(_slopeHit.point, _slopeHit.point + _slopeHit.normal);
    }
}
