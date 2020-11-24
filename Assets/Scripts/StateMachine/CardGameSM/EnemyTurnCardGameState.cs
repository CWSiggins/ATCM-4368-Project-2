using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 2f;

    [SerializeField] PlayerStats _player;

    [SerializeField] Creature _opponent;

    [SerializeField] DeckTester _deck;

    [SerializeField] GameObject _enemyThinking;
    [SerializeField] GameObject enemyTurnNode;
    [SerializeField] GameObject node2;

    [SerializeField] AudioClip transition;

    public void Start()
    {
        _enemyThinking.transform.position = node2.transform.position;
    }

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();
        LeanTween.move(_enemyThinking, enemyTurnNode.transform.position, 1);
        _deck.EnemyDraw();
        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
        AudioHelper.PlayClip2D(transition, 1f);
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
        LeanTween.move(_enemyThinking, node2.transform.position, 1);
        StartCoroutine("ResetText");
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

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(1);
        _enemyThinking.transform.position = node2.transform.position;
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
