using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(StartNode))]
public class StartNodeEditor : BaseNodeEditor
{
    StartNode _startNode;

    public override void OnBodyGUI()
    {
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Exit"));

        GUILayout.BeginVertical();

        base.OnBodyGUI();

        GUILayout.Space(10f);

        if (_startNode == null)
            _startNode = target as StartNode;

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("StartEvent"));

        if (GUILayout.Button("Raise Event"))
        {
            if (_startNode != null && _startNode.StartEvent != null)
                _startNode.StartEvent.Raise();
        }

        GUILayout.EndVertical();

        serializedObject.Update();
    }
}
