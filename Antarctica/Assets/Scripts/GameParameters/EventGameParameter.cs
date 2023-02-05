using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game Parameters/Event", fileName = "EventGameParameter")]
public class EventGameParameter : ScriptableObject
{
    public UnityAction Event;

    [ContextMenu("Raise Event")]
    public void Raise()
    {
        Event?.Invoke();
    }
}
