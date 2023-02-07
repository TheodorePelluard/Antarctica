using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedAsset<Languages, T>: ScriptableObject
{
    public T Data;
    [SerializeField] List<LocalizedElement> LocalizedElements;

    [System.Serializable]
    public class LocalizedElement
    {
        public Languages Language;
        public T Element;
    }

    public void UpdateValue(Languages lang)
    {
        Data = getLocalizedData(lang);
    }

    private T getLocalizedData(Languages lang)
    {
        for (int i = 0; i < LocalizedElements.Count; i++)
        {
            if (LocalizedElements[i].Language.Equals(lang))
            {
                return LocalizedElements[i].Element;
            }
        }

        Debug.LogError(name + " ne possède pas de valeurs pour la langue " + lang.ToString());
        return default(T);
    }
}
