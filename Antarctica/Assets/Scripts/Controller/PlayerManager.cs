using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Transform PlayerTransform;
    public Collider PlayerCollider;
    public Rigidbody PlayerRigidbody;

    [Header("PlayerScript")]
    public PlayerMovement PlayerMovement;
    public PlayerCamera PlayerCamera;
    public PlayerInteractor PlayerInteractor;
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
        EnablePlayerControl();
    }

    public void DisablePlayerControl()
    {
        ToggleRigibody(false);
        ToggleCollider(false);

        PlayerMovement.ToggleCanMove(false);
        PlayerCamera.ToggleCanLook(false);
        PlayerInteractor.ToggleCanInteract(false);
    }

    public void EnablePlayerControl()
    {
        ToggleRigibody(true);
        ToggleCollider(true);      

        PlayerMovement.ToggleCanMove(true);
        PlayerCamera.ToggleCanLook(true);
        PlayerInteractor.ToggleCanInteract(true);
    }

    public void ToggleCollider(bool state)
    {
        PlayerCollider.enabled = state;
    }

    public void ToggleRigibody(bool state)
    {
        PlayerRigidbody.useGravity = state;
    }
}
