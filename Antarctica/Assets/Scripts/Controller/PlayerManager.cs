using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] InputActionAsset Inputs;
    public static PlayerManager Instance;

    public Transform PlayerTransform;
    public Collider PlayerCollider;
    public Rigidbody PlayerRigidbody;

    [Header("PlayerScript")]
    public PlayerMovement PlayerMovement;
    [SerializeField] BoolGameParameter _playerCanMove;
    [SerializeField] BoolGameParameter _playerCanSprint;
    public PlayerCamera PlayerCamera;
    [SerializeField] BoolGameParameter _playerCanLook;
    public PlayerInteractor PlayerInteractor;
    [SerializeField] BoolGameParameter _playerCanInteract;
    public PlayerInteractorInterface PlayerInteractorInterface;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Inputs.Enable();
        EnablePlayerControl();
    }

    public void DisablePlayerControl()
    {
        ToggleRigibody(false);
        ToggleCollider(false);

        _playerCanMove.Set(false);
        _playerCanSprint.Set(false);
        _playerCanLook.Set(false);
        _playerCanInteract.Set(false);
    }

    public void EnablePlayerControl()
    {
        ToggleRigibody(true);
        ToggleCollider(true);

        _playerCanMove.Set(true);
        _playerCanSprint.Set(true);
        _playerCanLook.Set(true);
        _playerCanInteract.Set(true);
    }

    public void ToggleCollider(bool state)
    {
        PlayerCollider.enabled = state;
    }

    public void ToggleRigibody(bool state)
    {
        PlayerRigidbody.useGravity = state;
    }

    private void OnDestroy()
    {
        Inputs.Disable();
    }
}
