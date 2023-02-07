using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(250)]
[NodeTint("#1D6600")]
public class StartNode : BaseNode {

	public EventGameParameter StartEvent;
    public bool AutoStart = true;
	[Output] public int Exit;

    public override void OnEnterNode()
    {
        base.OnEnterNode();
        StartEvent.Event += onEventRaised;

        if(AutoStart)
            StartEvent.Raise();
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