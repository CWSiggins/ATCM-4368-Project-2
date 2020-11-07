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

    public void Start()
    {
        _lose = false;
        _losePanel.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
    }

    public override void Enter()
    {
        _lose = true;
        _losePanel.SetActive(true);
    }

    public override void Exit()
    {
        _lose = false;
        _losePanel.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene("CardTest");
    }
}
