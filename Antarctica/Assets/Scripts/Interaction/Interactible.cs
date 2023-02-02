using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class Interactible : MonoBehaviour, IInteractible
{
    [SerializeField] string _objectName;
    [Space]
    public UnityEvent OnEnterInteractState;
    public UnityEvent OnInteract;
    public UnityEvent OnExitInteractState;
    public UnityEvent OnResetInteraction;

    private void Start()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            gameObject.layer = LayerMask.NameToLayer("Interactible");
#endif
    }

    public string ReturnName()
    {
        return _objectName;
    }

    public virtual void EnterInteractState()
    {
        OnEnterInteractState?.Invoke();
    }

    public virtual void ExitInteractState()
    {
        OnExitInteractState?.Invoke();
    }

    public virtual void Interact()
    {
        OnInteract?.Invoke();
    }

    public virtual void ResetInteraction()
    {
        OnResetInteraction?.Invoke();
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        if (!Application.isPlaying)
            gameObject.layer = LayerMask.NameToLayer("Default");
    }


#endif
}
