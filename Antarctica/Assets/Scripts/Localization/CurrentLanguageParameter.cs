using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Languages { FR, EN, ES, DE, IT };

[CreateAssetMenu(menuName ="Localization/CurrentLanguage", fileName ="CurrentLanguage")]
public class CurrentLanguageParameter : ScriptableObject
{
    [SerializeField] Languages _language;
    public Languages Language
    {
        get { return _language; }
        set
        {
            if (value != _language)
            {
                _language = value;
                OnLanguageUpdated?.Invoke(_language);
            }
        }
    }

    public UnityAction<Languages> OnLanguageUpdated;

    private void OnEnable()
    {
        OnLanguageUpdated += onLanguageUpdated;
    }

    private void onLanguageUpdated(Languages lang)
    {
        _language = lang;
    }

    private void OnDisable()
    {
        OnLanguageUpdated -= onLanguageUpdated;
    }
}
