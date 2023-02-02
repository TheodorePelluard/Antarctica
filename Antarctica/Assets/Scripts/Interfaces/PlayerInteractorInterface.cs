using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractorInterface : MonoBehaviour
{
    bool _displayInterface = true;
    [SerializeField] PlayerInteractor Interactor;
    [Space]
    [SerializeField] TextMeshProUGUI ObjectNameText;
    [SerializeField] GameObject Cursor;
    [SerializeField] GameObject CursorInteraction;

    void OnEnable()
    {
        _displayInterface = true;
        Interactor.OnInteractibleChange += onInteractibleChange;
    }

    void onInteractibleChange(Interactible interactible)
    {
        if (!_displayInterface)
            return;

        if(interactible == null)
        {
            ObjectNameText.gameObject.SetActive(false);
            CursorInteraction.SetActive(false);
            Cursor.SetActive(true);
        }
        else
        {
            ObjectNameText.text = interactible.ReturnName();
            ObjectNameText.gameObject.SetActive(true);
            CursorInteraction.SetActive(true);
            Cursor.SetActive(false);
        }
    }

    public void HideInterface()
    {
        _displayInterface = false;

        ObjectNameText.gameObject.SetActive(false);
        CursorInteraction.SetActive(false);
        Cursor.SetActive(false);
    }

    public void ShowInterface()
    {
        _displayInterface = true;

        ObjectNameText.gameObject.SetActive(false);
        CursorInteraction.SetActive(false);
        Cursor.SetActive(true);
    }


    void OnDisable()
    {
        Interactor.OnInteractibleChange -= onInteractibleChange;
    }
}
