using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;

public class BaseNode : Node
{
    [HideInInspector] public DialogueGraphReader Reader;
    [HideInInspector] public bool Selected;
    [HideInInspector] public int OnEntryDynamicPortsCount = 0;
    [HideInInspector] public int OnExitDynamicPortsCount = 0;

    public virtual void OnEnterNode()
    {
        Debug.Log("Getting in: " + name);
        Selected = true;

        if(OnEntryDynamicPortsCount > 0)
        {
            for (int i = 0; i < OnEntryDynamicPortsCount; i++)
            {
                NodePort port = GetOutputPort("OnEntry_" + i).Connection;
                BaseNode node = port.node as BaseNode;
                node.OnEnterNode();

                node.OnExitNode();
            }
        }
    }

    public virtual void OnExitNode()
    {
        Debug.Log("Exiting: " + name);
        Selected = false;

        if (OnEntryDynamicPortsCount > 0)
        {
            for (int i = 0; i < OnExitDynamicPortsCount; i++)
            {
                NodePort port = GetOutputPort("OnExit_" + i).Connection;
                BaseNode node = port.node as BaseNode;
                node.OnEnterNode();

                node.OnExitNode();
            }
        }
    }

    public void EditorNextNode()
    {
        Reader.NextNode();
    }

    public void AddDynamicOnEntryPort()
    {
        AddDynamicOutput(typeof(int), fieldName: "OnEntry_" + OnEntryDynamicPortsCount.ToString());
        OnEntryDynamicPortsCount++;
    }

    public void RemoveDynamicOnEntryPort()
    {
        OnEntryDynamicPortsCount = Mathf.Clamp(OnEntryDynamicPortsCount--, 0, OnEntryDynamicPortsCount);

        string portName = "OnEntry_" + OnEntryDynamicPortsCount.ToString();
        HasPort(portName);
        RemoveDynamicPort(portName);
    }

    public void ClearAllDynamicPorts()
    {
        OnEntryDynamicPortsCount = 0;
        OnExitDynamicPortsCount = 0;
        base.ClearDynamicPorts();
    }

    public void AddDynamicOnExitPort()
    {
        AddDynamicOutput(typeof(int), fieldName: "OnExit_" + OnExitDynamicPortsCount.ToString());
        OnExitDynamicPortsCount++;
    }

    public void RemoveDynamicOnExitPort()
    {
        OnExitDynamicPortsCount = Mathf.Clamp(OnExitDynamicPortsCount--, 0, OnExitDynamicPortsCount);

        string portName = "OnExit_" + OnExitDynamicPortsCount.ToString();
        HasPort(portName);
        RemoveDynamicPort(portName);
    }
}