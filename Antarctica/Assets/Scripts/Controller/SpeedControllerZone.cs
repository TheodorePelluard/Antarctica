using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControllerZone : MonoBehaviour
{
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] BoxCollider _triggerZone;

    [SerializeField] BoolGameParameter _playerCanSprintParameter;
    bool _currentPlayerCanSprintValue;
    [SerializeField] bool _playerCanSprint = false;
    [SerializeField] FloatGameParameter _playerMaxWalkSpeedParameter;
    float _currentPlayerMaxSpeedValue;
    [SerializeField] float _playerMaxWalkSpeed = 5f;


    public void OnTriggerEnter(Collider other)
    {
        if(((1<< other.gameObject.layer) & _playerLayer) != 0)
        {
            _currentPlayerCanSprintValue = _playerCanSprintParameter.Get();
            _currentPlayerMaxSpeedValue = _playerMaxWalkSpeedParameter.Get();

            _playerCanSprintParameter.Set(_playerCanSprint);
            _playerMaxWalkSpeedParameter.Set(_playerMaxWalkSpeed);
        }
    }


    public void OnTriggerExit()
    {
        _playerCanSprintParameter.Set(_currentPlayerCanSprintValue);
        _playerMaxWalkSpeedParameter.Set(_currentPlayerMaxSpeedValue);
    }
}
