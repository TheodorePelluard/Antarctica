using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueGraphReader : MonoBehaviour
{
    public StartNode startNode;
    public DialogueGraph Graph;

    private void Awake()
    {
        setupNodes();
    }

    private void setupNodes()
    {
        Graph.Current = startNode;
        Graph.Current.OnEnterNode();

        foreach (BaseNode node in Graph.nodes)
        {
            node.Reader = this;
        }
    }

    public void NextNode(string fieldName = "Exit")
    {
        Graph.Current.OnExitNode();

        NodePort port = Graph.Current.GetOutputPort(fieldName).Connection;

        if (port != null)
        {
            Graph.Current = port.node as BaseNode;
            Graph.Current.OnEnterNode();
        }
    }

    private void OnDisable()
    {
        foreach (BaseNode node in Graph.nodes)
        {
            node.OnExitNode();
        }
    }

    public void ReadDialogueNode(DialogueNode node)
    {
        Debug.Log(node.DialogueParameter.DialogueSubtitle.Data);

        if(node.AutoNext)
            StartCoroutine(DelayReading());

        IEnumerator DelayReading()
        {
            yield return new WaitForSeconds(2f);
            NextNode();
        }
    }
}
