using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTransformUpdater : MonoBehaviour
{
    [SerializeField] Transform _transformToFollow;

    private void Update()
    {
        if (_transformToFollow)
            transform.position = _transformToFollow.position;
    }
}
