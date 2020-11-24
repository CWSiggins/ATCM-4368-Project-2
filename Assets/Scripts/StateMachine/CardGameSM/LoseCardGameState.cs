using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseCardGameState : CardGameState
{
    [SerializeField] GameObject _losePanel;
    [SerializeField] Button _restartButton;
    public bool _lose = false;

    [SerializeField] GameObject node1;
    [SerializeField] GameObject loseNode;
    [SerializeField] GameObject node2;
    [SerializeField] AudioClip loseClip;

    public void Start()
    {
        _lose = false;
        _losePanel.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
        _losePanel.transform.position = node2.transform.position;
    }

    public override void Enter()
    {
        _lose = true;
        _losePanel.SetActive(true);
        LeanTween.move(_losePanel, loseNode.transform.position, 3);
        AudioHelper.PlayClip2D(loseClip, 1f);
    }

    public override void Exit()
    {
        _lose = false;
        StateMachine.ChangeState<MenuCardGameState>();
        LeanTween.move(_losePanel, node1.transform.position, 3);
    }

    private void Restart()
    {
        SceneManager.LoadScene("CardTest");
    }
}
