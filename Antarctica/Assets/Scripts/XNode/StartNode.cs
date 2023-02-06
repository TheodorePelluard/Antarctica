using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(250)]
[NodeTint("#1D6600")]
public class StartNode : BaseNode {

	public EventGameParameter StartEvent;
	[Output] public int Exit;

    public override void OnEnterNode()
    {
        base.OnEnterNode();
        StartEvent.Event += onEventRaised;
    }

    private void onEventRaised()
    {
        Reader.NextNode();
    }

    public override void OnExitNode()
    {
        base.OnExitNode();
        StartEvent.Event -= onEventRaised;
    }
}