using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;

    [SerializeField] PlayerStats _player;

    [SerializeField] Creature _opponent;

    [SerializeField] DeckTester _deck;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        _deck.EnemyDraw();
        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
    }

    public void Update()
    {
        if (_player._currentHealth <= 0)
        {
            StateMachine.ChangeState<LoseCardGameState>();
        }

        if (_opponent._currentHealth <= 0)
        {
            StateMachine.ChangeState<WinCardGameState>();
        }
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy Thinking...");
        yield return new WaitForSeconds(pauseDuration);
        _deck.EnemyPlay();
        Debug.Log("Enemy performs action");
        EnemyTurnEnded?.Invoke();
        //Enemy turn over. Go back to Player.
        StateMachine.ChangeState<PlayerTurnCardGameState>();
    }
}
