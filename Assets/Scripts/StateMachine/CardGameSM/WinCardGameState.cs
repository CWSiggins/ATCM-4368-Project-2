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

    public void Start()
    {
        _win = false;
        _winPanel.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
    }

    public override void Enter()
    {
        _win = true;
        _winPanel.SetActive(true);
    }

    public override void Exit()
    {
        _win = false;
        _winPanel.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene("CardTest");
    }
}
