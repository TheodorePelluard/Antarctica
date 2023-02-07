using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;


[CustomNodeEditor(typeof(BaseNode))]
public class BaseNodeEditor : NodeEditor
{
    BaseNode _baseNode;
    bool showNodeAction = false;

    public override void OnHeaderGUI()
    {
        if (_baseNode == null)
            _baseNode = target as BaseNode;

        if (_baseNode != null && _baseNode.Selected)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box(EditorGUIUtility.IconContent("d_Animation.Record@2x"), GUILayout.Width(20f), GUILayout.Height(20f));
            base.OnHeaderGUI();
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
        }
        else 
            base.OnHeaderGUI();
    }

    public override void OnBodyGUI()
    {
    }
}
