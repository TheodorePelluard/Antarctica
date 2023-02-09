using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SubtitleInterfaceManager : MonoBehaviour
{
    public static SubtitleInterfaceManager Instance { get; private set; }

    [SerializeField] GameObject _content;
    [SerializeField] TextMeshProUGUI _subtitleText;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(this);
    }

    public void EnableSubtitle(string subtitleString)
    {
        _subtitleText.text = subtitleString;
        _content.SetActive(true);
    }

    public void DisableSubtitle()
    {
        _content.SetActive(false);
    }

}
