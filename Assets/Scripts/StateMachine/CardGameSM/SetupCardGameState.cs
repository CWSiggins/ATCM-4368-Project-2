using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int _startingCardNumber = 10;
    [SerializeField] int _numberOfPlayers = 2;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");
        //CAN'T change state while still in Enter()/Exit() transition!
        //DON'T put ChangeState() here
        _activated = false;
    }

    public override void Tick()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }    
    }

    public override void Exit()
    {
        Debug.Log("Setup: Exiting...");
    }
}
