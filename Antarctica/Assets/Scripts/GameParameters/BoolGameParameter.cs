using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Parameters/Bool", fileName ="BoolGameParameter")]
public class BoolGameParameter : GameParameter<bool>
{
    [ContextMenu("Set to True")]
    void setValueToTrue()
    {
        Set(true);
    }

    [ContextMenu("Set to False")]
    void setValueToFalse()
    {
        Set(false);
    }
}
