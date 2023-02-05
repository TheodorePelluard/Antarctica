using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameParameter<T>: ScriptableObject
{
    [SerializeField] T _defaultValue;
    [SerializeField] T _value;
    public UnityAction<T, T> _onValueChanged;

    //private void Awake()
    //{
    //    ResetValue();
    //}

    public T Get()
    {
        return _value;
    }

    public void Set(T newValue)
    {
        _onValueChanged?.Invoke(newValue, _value);
        _value = newValue;
    }

    public void ResetValue()
    {
        Set(_defaultValue);
    }
}
