using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text _playerTurnTextUI = null;

    public int _playerTurnCount = 0;

    [SerializeField] PlayerStats _player;

    [SerializeField] Creature _opponent;

    [SerializeField] DeckTester _deck;

    [SerializeField] GameObject _playerTurn;
    [SerializeField] GameObject _endTurn;
    [SerializeField] GameObject playerTurnNode;
    [SerializeField] GameObject endTurnNode;
    [SerializeField] GameObject node1;

    [SerializeField] AudioClip transition;

    public void Start()
    {
        _playerTurn.transform.position = node1.transform.position;
        _endTurn.transform.position = node1.transform.position;
    }
    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
        _deck._cardIsPlayed = false;
        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        LeanTween.move(_playerTurn, playerTurnNode.transform.position, 1);
        LeanTween.move(_endTurn, endTurnNode.transform.position, 1);
        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        AudioHelper.PlayClip2D(transition, 1f);
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        LeanTween.move(_playerTurn, node1.transform.position, 1);
        LeanTween.move(_endTurn, node1.transform.position, 1);
        StartCoroutine("ResetText");
        Debug.Log("Player Turn: Exiting...");
    }

    public void Update()
    {
        if(_player._currentHealth <= 0)
        {
            StateMachine.ChangeState<LoseCardGameState>();
        }

        if(_opponent._currentHealth <= 0)
        {
            StateMachine.ChangeState<WinCardGameState>();
        }
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(1);
        _playerTurn.transform.position = node1.transform.position;
        _endTurn.transform.position = node1.transform.position;
    }
}
