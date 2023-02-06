using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : BaseNodeEditor
{
    DialogueNode _dialogueNode;

    public override void OnHeaderGUI()
    {
        base.OnHeaderGUI();
    }

    public override void OnBodyGUI()
    {
        if (_dialogueNode == null)
            _dialogueNode = target as DialogueNode;
        else
        {
            string newName = _dialogueNode.SpeakerName + ": " + _dialogueNode.DialogueLine;

            if (newName.Length > 24)
                newName = newName.Substring(0, 25) + "...";

            _dialogueNode.name = newName;
        }

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Entry"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Exit"));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();

        base.OnBodyGUI();

        GUILayout.Space(10f);

        GUILayout.EndVertical();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("SpeakerName"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueLine"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("AutoNext"));

        serializedObject.ApplyModifiedProperties();

        serializedObject.Update();
    }
}
