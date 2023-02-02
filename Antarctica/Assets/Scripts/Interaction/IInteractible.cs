using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public abstract void EnterInteractState();
    public abstract void Interact();
    public abstract void ExitInteractState();
    public abstract void ResetInteraction();
}
