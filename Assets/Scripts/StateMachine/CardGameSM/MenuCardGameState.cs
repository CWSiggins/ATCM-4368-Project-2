using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCardGameState : CardGameState
{
    [SerializeField] GameObject _menu;
    [SerializeField] Button _startButton;

    public bool start = false;

    public void Start()
    {
        start = false;
        _menu.SetActive(false);
        _startButton.onClick.AddListener(StartGame);
    }

    public override void Enter()
    {
        start = false;
        _menu.SetActive(true);
    }

    public override void Exit()
    {
        start = true;
        _menu.SetActive(false);
    }

    private void StartGame()
    {
        _menu.SetActive(false);
        start = true;
        StateMachine.ChangeState<SetupCardGameState>();
    }
}
