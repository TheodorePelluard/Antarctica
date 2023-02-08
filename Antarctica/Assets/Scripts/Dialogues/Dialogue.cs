using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Dialogues/Dialogue", fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] public LocalizedAudio DialogueClip;
    [SerializeField] public LocalizedText DialogueSubtitle;
    public float Delay = 0f;

#if UNITY_EDITOR
    [ContextMenu("Create Releated Assets")]
    public void CreateRelatedAssets()
    {
        if (DialogueClip == null)
        {
            LocalizedAudio localizedAudioClip = new LocalizedAudio();
            string newLocalizedAudioClipName = "LA" + name.Substring(2);
            localizedAudioClip.name = newLocalizedAudioClipName;
            AssetDatabase.CreateAsset(localizedAudioClip, string.Format("Assets/Parameters/Dialogues/LocalizedAudioClip/{0}.asset", newLocalizedAudioClipName));
            AssetDatabase.SaveAssets();

            DialogueClip = localizedAudioClip;
        }

        if (DialogueSubtitle == null)
        {
            LocalizedText localizedText = new LocalizedText();
            string newLocalizedTextName = "LT" + name.Substring(2);
            localizedText.name = newLocalizedTextName;
            AssetDatabase.CreateAsset(localizedText, string.Format("Assets/Parameters/Dialogues/LocalizedText/{0}.asset", newLocalizedTextName));
            AssetDatabase.SaveAssets();

            DialogueSubtitle = localizedText;
        }
    }

    [ContextMenu("GetCreatedAssets")]
    public void GetCreatedAssets()
    {
        string newLocalizedAudioClipName = "LA" + name.Substring(2);
        DialogueClip = (LocalizedAudio)AssetDatabase.LoadAssetAtPath(string.Format("Assets/Parameters/Dialogues/LocalizedAudioClip/{0}.asset", newLocalizedAudioClipName), typeof(LocalizedAudio));
        
        string newLocalizedTextName = "LT" + name.Substring(2);
        DialogueSubtitle = (LocalizedText)AssetDatabase.LoadAssetAtPath(string.Format("Assets/Parameters/Dialogues/LocalizedText/{0}.asset", newLocalizedTextName), typeof(LocalizedText));
    }
#endif
}
