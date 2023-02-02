using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    bool _canInteract = true;
    [SerializeField] Transform _camera;
    [Space]
    [SerializeField] InputActionReference Interact;
    [SerializeField] float _interactionDistance = 2f;
    [SerializeField] LayerMask InteractibleMask;
    RaycastHit _interactibleHit;
    Interactible _currentInteractible;
    [HideInInspector]
    public UnityAction<Interactible> OnInteractibleChange;
    bool _alreadyInteract;

    private void Start()
    {
        _alreadyInteract = false;
    }

    public void ToggleCanInteract(bool state)
    {
        _canInteract = state;
    }


    private void Update()
    {
        if (!_canInteract)
        {
            if (_currentInteractible != null)
            {
                _currentInteractible.ExitInteractState();
                _currentInteractible = null;
                _alreadyInteract = false;

                OnInteractibleChange?.Invoke(null);
            }

            return;
        }
        else
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out _interactibleHit, _interactionDistance, InteractibleMask))
            {
                if (_interactibleHit.transform.TryGetComponent(out Interactible interactible))
                {
                    if (_currentInteractible == null)
                    {
                        _currentInteractible = interactible;
                        _currentInteractible.EnterInteractState();

                        OnInteractibleChange?.Invoke(_currentInteractible);
                    }

                    if (inputInteraction() && _currentInteractible != null)
                    {
                        if (!_alreadyInteract)
                        {
                            _currentInteractible.Interact();
                            _alreadyInteract = true;
                        }
                    }
                }
            }
            else
            {
                if (_currentInteractible != null)
                {
                    _currentInteractible.ExitInteractState();
                    _currentInteractible = null;
                    _alreadyInteract = false;

                    OnInteractibleChange?.Invoke(null);
                }
            }
        }
    }

    bool inputInteraction()
    {
        if (Interact.action.ReadValue<float>() > 0)
            return true;
        else
        {
            _alreadyInteract = false;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_camera.position, _camera.position + _camera.forward * _interactionDistance);
    }
}
