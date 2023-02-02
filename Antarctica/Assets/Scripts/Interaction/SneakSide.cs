using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakSide : Interactible
{
    [SerializeField] Sneak _sneak;
    [Space]
    [SerializeField] bool _rightSide = true;
    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;
    Vector3 _gizmosEnterOrientation = new Vector3(0f, 0.1f, 0f);
    Vector3 _gizmosExitOrientation = new Vector3(0f, 1.8f, 0f);

    public override void Interact()
    {
        base.Interact();        
        _sneak.Interact(_startPoint, _endPoint, _rightSide);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_startPoint)
            Gizmos.DrawLine(_startPoint.position + _gizmosEnterOrientation, _startPoint.position + _startPoint.forward + _gizmosEnterOrientation);

        if (_endPoint)
            Gizmos.DrawLine(_endPoint.position + _gizmosEnterOrientation, _endPoint.position + _endPoint.forward + _gizmosEnterOrientation);
    }
}
