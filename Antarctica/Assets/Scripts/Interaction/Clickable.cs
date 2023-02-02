using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : Interactible
{
    public override void EnterInteractState()
    {
        base.EnterInteractState();
        Debug.Log("Enter Interaction State");
    }

    public override void Interact()
    {
        base.Interact();

        Debug.Log("Currently Interacting with Object");
    }

    public override void ExitInteractState()
    {
        base.ExitInteractState();

        Debug.Log("Exiting interaction");
    }
}
