using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

public class DialogueGraphReader : MonoBehaviour
{
    public StartNode startNode;
    public DialogueGraph Graph;
    [Header("Dialogues")]
    public AudioSource DialogueSource;
    Coroutine _readDialogueNodeCoroutine;
    SubtitleInterfaceManager _subtitleInterfaceManager;

    private void OnEnable()
    {
        _subtitleInterfaceManager = SubtitleInterfaceManager.Instance;
    }

    private void Start()
    {
        setupNodes();
    }

    private void setupNodes()
    {
        foreach (BaseNode node in Graph.nodes)
        {
            node.Reader = this;
        }

        Graph.Current = startNode;
        Graph.Current.OnEnterNode();
    }

    public void NextNode(string fieldName = "Exit")
    {
        Graph.Current.OnExitNode();

        NodePort port = Graph.Current.GetOutputPort(fieldName).Connection;

        if (port != null)
        {
            Graph.Current = port.node as BaseNode;
            Graph.Current.OnEnterNode();
        }
    }


    public void ReadDialogueNode(DialogueNode node)
    {
        if(_readDialogueNodeCoroutine != null)
        {
            StopCoroutine(_readDialogueNodeCoroutine);
            _readDialogueNodeCoroutine = null;
        }

        if (_readDialogueNodeCoroutine == null && node.DialogueParameter.DialogueClip.Data != null)
            _readDialogueNodeCoroutine = StartCoroutine(ReadDialogueNodeCoroutine());

        IEnumerator ReadDialogueNodeCoroutine()
        {
            Debug.Log(node.DialogueParameter.DialogueSubtitle.Data);

            DialogueSource.clip = node.DialogueParameter.DialogueClip.Data;
            DialogueSource.Play();
            _subtitleInterfaceManager.EnableSubtitle(node.DialogueParameter.DialogueSubtitle.Data);
            yield return new WaitForSeconds(node.DialogueParameter.DialogueClip.Data.length);

            if(node.DialogueParameter.Delay != 0)
                _subtitleInterfaceManager.DisableSubtitle();

            yield return new WaitForSeconds(node.DialogueParameter.Delay);

            DialogueSource.Stop();
            DialogueSource.clip = null;

            if (node.AutoNext)
                NextNode();

            _readDialogueNodeCoroutine = null;
        }
    }

    //async void waitForDialogueToComplete(float dialogueDuration)
    //{
    //    await Task.Delay((int)(dialogueDuration*100f));
    //}

    private void OnDisable()
    {
        foreach (BaseNode node in Graph.nodes)
        {
            node.OnExitNode();
        }

        if(_readDialogueNodeCoroutine != null)
        {
            StopCoroutine(_readDialogueNodeCoroutine);
            _readDialogueNodeCoroutine = null;
        }
    }
}
