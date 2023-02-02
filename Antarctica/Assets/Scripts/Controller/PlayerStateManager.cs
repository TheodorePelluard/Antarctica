using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerState {Idle, Walking, Sprinting, LockPosition, NoControl }
public class PlayerStateManager: MonoBehaviour
{
    public static PlayerStateManager Instance;

    public UnityAction<PlayerState> OnPlayerStateChange;
    public PlayerState CurrentPlayerState;
    private PlayerState _currentPlayerState
    {
        get 
        { 
            return CurrentPlayerState; 
        }
        set 
        { 
            if(CurrentPlayerState != _currentPlayerState)
            {
                _currentPlayerState = CurrentPlayerState;
                OnPlayerStateChange?.Invoke(CurrentPlayerState);
            }
        }
    }


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(this);
    }
}
