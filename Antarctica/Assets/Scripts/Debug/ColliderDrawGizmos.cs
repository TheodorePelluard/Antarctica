using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderDrawGizmos : MonoBehaviour
{
    [ColorUsage(false)]
    public Color Color;
    BoxCollider _boxCollider;

    private void OnEnable()
    {
        
    }

    private void OnValidate()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.r, Color.g, Color.b, 0.65f);
        Gizmos.matrix = transform.localToWorldMatrix;

        if (_boxCollider && isActiveAndEnabled)
            Gizmos.DrawCube(_boxCollider.center, _boxCollider.size);
    }
}
