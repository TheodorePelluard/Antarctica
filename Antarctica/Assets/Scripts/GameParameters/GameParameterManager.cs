using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameterManager : MonoBehaviour
{
    public List<BoolGameParameter> BoolParameters;
    public List<FloatGameParameter> FloatParameters;

    public void Awake()
    {
        for (int i = 0; i < BoolParameters.Count; i++)
        {
            BoolParameters[i].ResetValue();
        }

        for (int i = 0; i < FloatParameters.Count; i++)
        {
            FloatParameters[i].ResetValue();
        }
    }
}
