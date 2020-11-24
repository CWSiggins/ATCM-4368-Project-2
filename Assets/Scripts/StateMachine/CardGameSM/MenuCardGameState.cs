using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCardGameState : CardGameState
{
    [SerializeField] GameObject _menu;
    [SerializeField] Button _startButton;
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioClip menu;
    [SerializeField] AudioClip game;

    [SerializeField] GameObject node1;
    [SerializeField] GameObject menuNode;
    [SerializeField] GameObject node2;

    public bool start = false;

    public void Start()
    {
        start = false;
        _menu.SetActive(true);
        _startButton.onClick.AddListener(StartGame);
        _menu.transform.position = node1.transform.position;
    }

    public override void Enter()
    {
        start = false;
        _menu.SetActive(true);
        menuMusic.Play();
        gameMusic.Stop();
        LeanTween.move(_menu, menuNode.transform.position, 1);
    }

    public override void Exit()
    {
        start = true;
        gameMusic.Play();
        menuMusic.Stop();
        LeanTween.move(_menu, node2.transform.position, 1);
    }

    private void StartGame()
    {
        _menu.SetActive(false);
        start = true;
        StateMachine.ChangeState<SetupCardGameState>();
    }
}
