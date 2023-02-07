using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(BoolNode))]
public class BoolNodeEditor : BaseNodeEditor
{
    BoolNode _boolNode;

    public override void OnBodyGUI()
    {
        serializedObject.Update();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Entry"));

        GUILayout.BeginVertical();

        base.OnBodyGUI();

        GUILayout.Space(10f);

        if (_boolNode == null)
            _boolNode = target as BoolNode;

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Bool"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("NewBoolValue"));

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
