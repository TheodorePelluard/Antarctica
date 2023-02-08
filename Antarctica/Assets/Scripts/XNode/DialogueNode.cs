using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(250)]
public class DialogueNode : BaseNode
{
    [Input] public int Entry;
    [Output] public int Exit;
    public Dialogue DialogueParameter;
    public float Delay = 0f;
    public bool AutoNext = false;

    public override void OnEnterNode()
    {
        base.OnEnterNode();
        Reader.ReadDialogueNode(this);
    }

    public override void OnExitNode()
    {
        base.OnExitNode();
    }
}
