using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(250)]
[NodeTint("#851305")]
public class ExitNode : BaseNode {

	public EventGameParameter ExitEvent;
	[Input] public int Entry;

    public override void OnEnterNode()
    {
        base.OnEnterNode();

        if(ExitEvent)
            ExitEvent.Raise();
    }

    public override void OnExitNode()
    {
        base.OnExitNode();
    }
}