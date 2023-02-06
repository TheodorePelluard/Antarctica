using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class GameParameterManager : MonoBehaviour
{
    public BoolGameParameter[] BoolParameters;
    public FloatGameParameter[] FloatParameters;
    public EventGameParameter[] EventParameters;

    public void Awake()
    {
        for (int i = 0; i < BoolParameters.Length; i++)
        {
            BoolParameters[i].ResetValue();
        }

        for (int i = 0; i < FloatParameters.Length; i++)
        {
            FloatParameters[i].ResetValue();
        }
    }
#if UNITY_EDITOR
    public void OnValidate()
    {
        BoolParameters = Resources.FindObjectsOfTypeAll<BoolGameParameter>();
        FloatParameters = Resources.FindObjectsOfTypeAll<FloatGameParameter>();
        EventParameters = Resources.FindObjectsOfTypeAll<EventGameParameter>();
    }
#endif
}
