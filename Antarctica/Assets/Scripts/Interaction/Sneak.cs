using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Sneak : MonoBehaviour
{
    [SerializeField] float _sneakSpeed = 600f;
    [SerializeField] float _sneakEntrySpeed = 1.5f;
    [SerializeField] float _sneakExitSpeed = 1.5f;
    Coroutine _sneakCoroutine;
    [SerializeField] float _angleInCorridor = 45f;
    [SerializeField] float _speedToRotateInCorridor = 0.8f;
    Coroutine _lerpToOtherSideCoroutine;
    [Space]
    public bool CanEnterRightSide = true;
    [SerializeField] SneakSide sideRight;
    public bool CanEnterLeftSide = true;
    [SerializeField] SneakSide sideLeft;
    bool _isPlayerMovingRight = false;


    private void Start()
    {
        sideRight.gameObject.SetActive(CanEnterRightSide);
        sideLeft.gameObject.SetActive(CanEnterLeftSide);
    }

    public void Interact(Transform startPoint, Transform endPoint, bool rightSide)
    {
        _isPlayerMovingRight = rightSide;
        bool _currentIsPlayerMovingRight = _isPlayerMovingRight;

        if (_sneakCoroutine == null)
            StartCoroutine(sneakSequence());

        IEnumerator sneakSequence()
        {
            Debug.Log("Player Sneak");

            PlayerManager playerManager = PlayerManager.Instance;

            float t = 0.01f;
            Transform player = playerManager.PlayerTransform;

            playerManager.DisablePlayerControl();
            playerManager.PlayerInteractorInterface.HideInterface();

            yield return StartCoroutine(lerpToStartPoint(startPoint));

            while (t > 0 && t < 1f)
            {
                if (playerManager.PlayerMovement.GetLateralMovement() > 0)
                {
                    t += 1 / (_sneakSpeed * (rightSide ? 1 : -1));
                    _currentIsPlayerMovingRight = true;
                }
                else if (playerManager.PlayerMovement.GetLateralMovement() < 0)
                {
                    t -= 1 / (_sneakSpeed * (rightSide ? 1 : -1));
                    _currentIsPlayerMovingRight = false;
                }
                else
                    yield return null;

                player.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

                if(_currentIsPlayerMovingRight != _isPlayerMovingRight)
                {
                    if (_lerpToOtherSideCoroutine != null)
                        StopCoroutine(_lerpToOtherSideCoroutine);

                    _lerpToOtherSideCoroutine = StartCoroutine(lerpCameraToOtherSide(startPoint));

                    _isPlayerMovingRight = _currentIsPlayerMovingRight;
                }

                yield return null;
            }

            playerManager.ToggleCollider(true); //Toggle Collider to true early to avoid player falling into the ground

            if (t > 1f) //Exit on the other side
                yield return StartCoroutine(lerpToEndPoint(endPoint, !rightSide));
            else if (t < 0f)
                yield return StartCoroutine(lerpToEndPoint(startPoint, rightSide));

            playerManager.PlayerInteractorInterface.ShowInterface();
            playerManager.EnablePlayerControl();
            _sneakCoroutine = null;
        }
    }

    public void ToggleSide(bool newRightState, bool newLeftState)
    {
        CanEnterRightSide = newRightState;
        sideRight.gameObject.SetActive(CanEnterRightSide);
        CanEnterLeftSide = newLeftState;
        sideLeft.gameObject.SetActive(CanEnterLeftSide);
    }

    IEnumerator lerpToStartPoint(Transform startPoint)
    {
        float t = 0f;
        Transform player = PlayerManager.Instance.PlayerTransform;
        Quaternion _cameraStartAngle = PlayerManager.Instance.PlayerCamera.Camera.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * _sneakEntrySpeed;
            player.position = Vector3.Lerp(player.position, startPoint.position, t);
            player.rotation = Quaternion.Lerp(player.rotation, startPoint.rotation, t);

            PlayerManager.Instance.PlayerCamera.SetCameraRotation(Quaternion.Lerp(_cameraStartAngle, startPoint.rotation * Quaternion.Euler(0f, _angleInCorridor * (_isPlayerMovingRight ? 1 : -1), 0f), t));

            yield return null;
        }
    }

    IEnumerator lerpToEndPoint(Transform transformRotationPoint, bool rightSide)
    {
        if (_lerpToOtherSideCoroutine != null)
            StopCoroutine(_lerpToOtherSideCoroutine);

        Transform player = PlayerManager.Instance.PlayerTransform;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * _sneakExitSpeed;
            PlayerManager.Instance.PlayerCamera.SetCameraRotation
                (Quaternion.Lerp(player.rotation, Quaternion.Euler(transformRotationPoint.right) * Quaternion.AngleAxis(rightSide ? 180 : 0, Vector3.up), t));
            yield return null;
        }
    }

    IEnumerator lerpCameraToOtherSide(Transform startPoint)
    {
        float t = 0f;
        Transform player = PlayerManager.Instance.PlayerTransform;

        while (t < 1)
        {
            t += Time.deltaTime * _speedToRotateInCorridor;

            PlayerManager.Instance.PlayerCamera.SetCameraRotation
                (Quaternion.Lerp(player.rotation, startPoint.rotation * Quaternion.Euler(0f, _angleInCorridor * (_isPlayerMovingRight ? 1 : -1), 0f), t));

            yield return null;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        sideRight.gameObject.SetActive(CanEnterRightSide);
        sideLeft.gameObject.SetActive(CanEnterLeftSide);
    }
#endif
}
