using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class LanguageManager : MonoBehaviour
{
    [SerializeField] bool _forceDebugLanguage;
    [SerializeField] Languages _debugLanguage;
    [Space]
    [SerializeField] CurrentLanguageParameter _currentLanguageParameter;
    [Space]
    public List<LocalizedAsset<Languages, AudioClip>> LocalizedAudioClips;
    public List<LocalizedAsset<Languages, string>> LocalizedTexts;

    private void Start()
    {
        if (Application.isPlaying)
        {
            if (_forceDebugLanguage)
                _currentLanguageParameter.Language = _debugLanguage;
            else
            {
                if (Application.systemLanguage == SystemLanguage.French)
                    _currentLanguageParameter.Language = Languages.FR;
                else if (Application.systemLanguage == SystemLanguage.Spanish)
                    _currentLanguageParameter.Language = Languages.ES;
                else if (Application.systemLanguage == SystemLanguage.German)
                    _currentLanguageParameter.Language = Languages.DE;
                else if (Application.systemLanguage == SystemLanguage.Italian)
                    _currentLanguageParameter.Language = Languages.IT;
                else
                    _currentLanguageParameter.Language = Languages.EN;
            }

            for (int i = 0; i < LocalizedAudioClips.Count; i++)
            {
                LocalizedAudioClips[i].UpdateValue(_currentLanguageParameter.Language);
            }

            for (int i = 0; i < LocalizedTexts.Count; i++)
            {
                LocalizedTexts[i].UpdateValue(_currentLanguageParameter.Language);
            }
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        LocalizedAudioClips.Clear();

        string[] localizedAudiofiles = Directory.GetFiles("Assets/Parameters/Dialogues/LocalizedAudioClip", "*.asset", SearchOption.TopDirectoryOnly);

        foreach(var localizedAudioFile in localizedAudiofiles)
        {
            LocalizedAudio localizedAudioClip = (LocalizedAudio)AssetDatabase.LoadAssetAtPath(localizedAudioFile, typeof(LocalizedAudio));
            LocalizedAudioClips.Add(localizedAudioClip);
        }

        LocalizedTexts.Clear();

        string[] localizedTextfiles = Directory.GetFiles("Assets/Parameters/Dialogues/LocalizedText", "*.asset", SearchOption.TopDirectoryOnly);

        foreach (var localizedTextFile in localizedTextfiles)
        {
            LocalizedText localizedText = (LocalizedText)AssetDatabase.LoadAssetAtPath(localizedTextFile, typeof(LocalizedText));
            LocalizedTexts.Add(localizedText);
        }
    }
#endif
}
