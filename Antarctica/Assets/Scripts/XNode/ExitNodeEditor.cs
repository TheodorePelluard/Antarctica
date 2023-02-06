using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(ExitNode))]
public class ExitNodeEditor : BaseNodeEditor
{
    ExitNode _exitNode;

    public override void OnBodyGUI()
    {
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Entry"));

        GUILayout.BeginVertical();

        base.OnBodyGUI();

        GUILayout.Space(10f);

        if (_exitNode == null)
            _exitNode = target as ExitNode;

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ExitEvent"));

        GUILayout.EndVertical();

        serializedObject.Update();
    }
}
