using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCardGameState : CardGameState
{
    [SerializeField] GameObject _winPanel;
    [SerializeField] Button _restartButton;
    public bool _win = false;

    [SerializeField] GameObject node1;
    [SerializeField] GameObject winNode;
    [SerializeField] GameObject node2;

    [SerializeField] AudioClip winClip;

    public void Start()
    {
        _win = false;
        _winPanel.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
        _winPanel.transform.position = node1.transform.position;
    }

    public override void Enter()
    {
        _win = true;
        _winPanel.SetActive(true);
        LeanTween.move(_winPanel, winNode.transform.position, 3);
        AudioHelper.PlayClip2D(winClip, 1f);
    }

    public override void Exit()
    {
        _win = false;
        StateMachine.ChangeState<MenuCardGameState>();
        LeanTween.move(_winPanel, node2.transform.position, 3);
    }

    private void Restart()
    {
        SceneManager.LoadScene("CardTest");
    }
}
