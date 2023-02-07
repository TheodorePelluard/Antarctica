using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public LocalizedAsset<Languages, AudioClip>[] LocalizedAudioClips;
    public LocalizedAsset<Languages, string>[] LocalizedTexts;

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

            for (int i = 0; i < LocalizedAudioClips.Length; i++)
            {
                LocalizedAudioClips[i].UpdateValue(_currentLanguageParameter.Language);
            }

            for (int i = 0; i < LocalizedTexts.Length; i++)
            {
                LocalizedTexts[i].UpdateValue(_currentLanguageParameter.Language);
            }
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        LocalizedAudioClips = Resources.FindObjectsOfTypeAll<LocalizedAsset<Languages, AudioClip>>();
        LocalizedTexts = Resources.FindObjectsOfTypeAll<LocalizedAsset<Languages, string>>();
    }
#endif
}
