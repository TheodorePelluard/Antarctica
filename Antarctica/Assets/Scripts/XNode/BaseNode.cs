using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;

public class BaseNode : Node
{

    [HideInInspector] public DialogueGraphReader Reader;
    [HideInInspector] public bool Selected;
    [SerializeField] UnityEvent OnEnterNodeEvent;
    [SerializeField] UnityEvent OnExitNodeEvent;

    public virtual void OnEnterNode()
    {
        Debug.Log("Getting in: " + name);
        Selected = true;
        OnEnterNodeEvent?.Invoke();
    }

    public virtual void OnExitNode()
    {
        Debug.Log("Exiting: " + name);
        Selected = false;
        OnExitNodeEvent?.Invoke();
    }

    public void EditorNextNode()
    {
        Reader.NextNode();
    }
}