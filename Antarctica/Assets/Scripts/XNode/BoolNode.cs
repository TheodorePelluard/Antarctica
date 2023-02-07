using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(250)]
public class BoolNode : BaseNode {

	public BoolGameParameter Bool;
    public bool NewBoolValue;
	[Input] public int Entry;

    public override void OnEnterNode()
    {
        base.OnEnterNode();

        if (Bool)
            Bool.Set(NewBoolValue);
    }
}