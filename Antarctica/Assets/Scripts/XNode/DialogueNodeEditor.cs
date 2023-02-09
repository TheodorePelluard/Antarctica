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
    bool _showOnEntryEvent = true;
    bool _showOnExitEvent = true;

    public override void OnHeaderGUI()
    {
        if (_dialogueNode != null && _dialogueNode.DialogueParameter != null)
            base.OnHeaderGUI();
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box(EditorGUIUtility.IconContent("CollabConflict Icon"), GUILayout.Width(20f), GUILayout.Height(20f));
            base.OnHeaderGUI();
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
        }
    }

    public override void OnBodyGUI()
    {
        serializedObject.Update();

        if (_dialogueNode == null)
            _dialogueNode = target as DialogueNode;
        else if (_dialogueNode.DialogueParameter != null)
        {
            string newName = _dialogueNode.DialogueParameter.name;

            if (newName.Length > 24)
                newName = newName.Substring(0, 25) + "...";

            _dialogueNode.name = newName;
        }
        else
            _dialogueNode.name = "Dialogue";

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Entry"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Exit"));
        GUILayout.EndHorizontal();

        base.OnBodyGUI();

        GUILayout.BeginHorizontal();
        if (_dialogueNode.DialogueParameter && _dialogueNode.DialogueParameter.DialogueSubtitle)
        {
            string labelContent = _dialogueNode.DialogueParameter.DialogueSubtitle.Data;

            if (labelContent.Length > 35)
                labelContent = labelContent.Substring(0, 35) + "...";

            GUILayout.Label(string.Format("'{0}'", labelContent));
        }

        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueParameter"));

        if (_dialogueNode != null && _dialogueNode.DialogueParameter != null)
        {
            var delay = serializedObject.FindProperty("Delay").floatValue;
            delay = EditorGUILayout.FloatField("Delay", delay);

            _dialogueNode.Delay = delay;
            _dialogueNode.DialogueParameter.Delay = delay;

            //EditorGUILayout.PropertyField(serializedObject.FindProperty("Delay"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("AutoNext"));
        }

        GUILayout.Space(10f);

#region Dynamic OnEntry

        string foldOutNameEntry = string.Format("On Entry Event [{0}]",_dialogueNode.OnEntryDynamicPortsCount);
        _showOnEntryEvent = EditorGUILayout.BeginFoldoutHeaderGroup(_showOnEntryEvent, foldOutNameEntry);

        if (_showOnEntryEvent)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_close_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.ClearAllDynamicPorts();
            GUILayout.Space(30f);
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_min_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.RemoveDynamicOnEntryPort();
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_max_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.AddDynamicOnEntryPort();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            for (int i = 0; i < _dialogueNode.OnEntryDynamicPortsCount; i++)
            {
                string portName = "OnEntry_" + i;
                if (_dialogueNode.HasPort(portName))
                    NodeEditorGUILayout.PortField(_dialogueNode.GetPort(portName));
            }
            GUILayout.EndVertical();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion

#region Dynamic OnExit

        string foldOutNameExit = string.Format("On Exit Event [{0}]", _dialogueNode.OnExitDynamicPortsCount);
        _showOnExitEvent = EditorGUILayout.BeginFoldoutHeaderGroup(_showOnExitEvent, foldOutNameExit);

        if (_showOnExitEvent)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_close_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.ClearAllDynamicPorts();
            GUILayout.Space(30f);
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_min_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.RemoveDynamicOnExitPort();
            if (GUILayout.Button(EditorGUIUtility.IconContent("winbtn_mac_max_h@2x"), GUILayout.Height(20f)))
                _dialogueNode.AddDynamicOnExitPort();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            for (int i = 0; i < _dialogueNode.OnExitDynamicPortsCount; i++)
            {
                string portName = "OnExit_" + i;
                if (_dialogueNode.HasPort(portName))
                    NodeEditorGUILayout.PortField(_dialogueNode.GetPort(portName));
            }
            GUILayout.EndVertical();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        #endregion

        serializedObject.ApplyModifiedProperties();
    }
}
