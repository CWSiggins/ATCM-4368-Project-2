using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckTester : MonoBehaviour
{
    [SerializeField] List<AbilityCardData> _abilityDeckConfig = new List<AbilityCardData>();
    [SerializeField] AbilityCardView _abilityCardView = null;
    Deck<AbilityCard> _abilityDeck = new Deck<AbilityCard>();
    Deck<AbilityCard> _abilityDiscard = new Deck<AbilityCard>();

    Deck<AbilityCard> _playerHand = new Deck<AbilityCard>();
    Deck<AbilityCard> _enemyHand = new Deck<AbilityCard>();

    [SerializeField] GameObject card1Panel;
    [SerializeField] GameObject card2Panel;
    [SerializeField] GameObject card3Panel;
    [SerializeField] GameObject deckPanel;
    [SerializeField] GameObject card1Node;
    [SerializeField] GameObject card2Node;
    [SerializeField] GameObject card3Node;
    [SerializeField] Button card1Button;
    [SerializeField] Button card2Button;
    [SerializeField] Button card3Button;
    [SerializeField] Button drawButton;
    [SerializeField] Text deckCount;
    [SerializeField] Text discardCount;

    [SerializeField] GameObject enemyCard1;
    [SerializeField] GameObject enemyCard2;
    [SerializeField] GameObject enemyCard3;
    [SerializeField] GameObject enemyCard1Node;
    [SerializeField] GameObject enemyCard2Node;
    [SerializeField] GameObject enemyCard3Node;

    [SerializeField] TargetController target;
    [SerializeField] PlayerTurnCardGameState playerTurnNumber;

    public bool _cardIsPlayed;

    private void Start()
    {
        SetupAbilityDeck();

        target.targetSelected = false;
        _cardIsPlayed = false;

        drawButton.onClick.AddListener(Draw);

        card1Panel.SetActive(false);
        card2Panel.SetActive(false);
        card3Panel.SetActive(false);

        card1Button.onClick.AddListener(PlayCard1);
        card2Button.onClick.AddListener(PlayCard2);
        card3Button.onClick.AddListener(PlayCard3);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            PrintEnemyHand();
        }

        deckCount.text = ("" + _abilityDeck.Count);
        discardCount.text = ("" + _abilityDiscard.Count);

        Display();
        EnemyDisplay();
    }

    private void SetupAbilityDeck()
    {
        foreach(AbilityCardData abilityData in _abilityDeckConfig)
        {
            AbilityCard newAbilityCard = new AbilityCard(abilityData);
            _abilityDeck.Add(newAbilityCard);
        }

        _abilityDeck.Shuffle();
    }

    private void Display()
    {
        if (_playerHand.Count == 0)
        {
            card1Panel.SetActive(false);
            card2Panel.SetActive(false);
            card3Panel.SetActive(false);
        }

        if (_playerHand.Count >= 1)
        {
            AbilityCard playerCard1 = _playerHand.GetCard(0);
            card1Panel.SetActive(true);
            _abilityCardView.Display1(playerCard1);
        }

        if (_playerHand.Count == 1)
        {
            card2Panel.SetActive(false);
            card3Panel.SetActive(false);
        }

        if (_playerHand.Count >= 2)
        {
            AbilityCard playerCard2 = _playerHand.GetCard(1);
            card2Panel.SetActive(true);
            _abilityCardView.Display2(playerCard2);
        }

        if (_playerHand.Count == 2)
        {
            card3Panel.SetActive(false);
        }

        if (_playerHand.Count == 3)
        {
            AbilityCard playerCard3 = _playerHand.GetCard(2);
            _abilityCardView.Display3(playerCard3);
            card3Panel.SetActive(true);
        }
    }

    private void EnemyDisplay()
    {
        if(_enemyHand.Count == 0)
        {
            enemyCard1.SetActive(false);
            enemyCard2.SetActive(false);
            enemyCard3.SetActive(false);
        }

        if(_enemyHand.Count == 2)
        {
            enemyCard1.SetActive(true);
            enemyCard2.SetActive(true);
            enemyCard3.SetActive(false);
        }

        if(_enemyHand.Count == 3)
        {
            enemyCard1.SetActive(true);
            enemyCard2.SetActive(true);
            enemyCard3.SetActive(true);
        }
    }

    private void Draw()
    {
        if (_playerHand.Count < 3)
        {
            AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
            Debug.Log("Draw card: " + newCard.Name);
            _playerHand.Add(newCard, DeckPosition.Top);
            DrawAnimation();
        }

        if (_abilityDeck.Count == 0)
        {
            for(int i = _abilityDiscard.Count; i >= 0; i--)
            {
                AbilityCard reAdd = _abilityDiscard.Draw(DeckPosition.Top);
                _abilityDeck.Add(reAdd, DeckPosition.Top);
                _abilityDeck.Shuffle();
            }
        }

    }

    public void EnemyDraw()
    {
        while(_enemyHand.Count < 3)
        {
            AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
            Debug.Log("Enemy draws card: " + newCard.Name);
            _enemyHand.Add(newCard, DeckPosition.Top);
            EnemyDrawAnimation();
        }

        if (_abilityDeck.Count == 0)
        {
            for (int i = _abilityDiscard.Count; i > 0; i--)
            {
                AbilityCard reAdd = _abilityDiscard.Draw(DeckPosition.Top);
                _abilityDeck.Add(reAdd, DeckPosition.Top);
                _abilityDeck.Shuffle();
            }
        }
    }

    public void EnemyPlay()
    {
        if(_enemyHand.Count == 3)
        {
            int pickCard = Random.Range(0, (_enemyHand.Count - 1));
            AbilityCard enemyCard = _enemyHand.GetCard(pickCard);
            if(enemyCard.Type == "Attack")
            {
                target.TargetPlayer();
            }
            else
            {
                target.TargetEnemy();
            }
            Debug.Log(enemyCard.Name);
            enemyCard.Play();
            _enemyHand.Remove(pickCard);
            _abilityDiscard.Add(enemyCard);
        }
        target.targetSelected = false;
    }

    private void PrintEnemyHand()
    {
        for (int i = 0; i < _enemyHand.Count; i++)
        {
            Debug.Log("Enemy Hand Card: " + _enemyHand.GetCard(i).Name);
        }
    }

    void EnemyDrawAnimation()
    {
        if(_enemyHand.Count == 3)
        {
            if(playerTurnNumber._playerTurnCount > 1)
            {
                enemyCard3.transform.position = deckPanel.transform.position;
                enemyCard3.transform.localScale = deckPanel.transform.localScale;
                LeanTween.move(enemyCard1, enemyCard1Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard1, enemyCard1Node.transform.localScale, 0.5f);
                LeanTween.move(enemyCard2, enemyCard2Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard2, enemyCard2Node.transform.localScale, 0.5f);
                LeanTween.move(enemyCard3, enemyCard3Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard3, enemyCard3Node.transform.localScale, 0.5f);
            }
            else
            {
                enemyCard1.transform.position = deckPanel.transform.position;
                enemyCard1.transform.localScale = deckPanel.transform.localScale;
                enemyCard2.transform.position = deckPanel.transform.position;
                enemyCard2.transform.localScale = deckPanel.transform.localScale;
                enemyCard3.transform.position = deckPanel.transform.position;
                enemyCard3.transform.localScale = deckPanel.transform.localScale;
                LeanTween.move(enemyCard1, enemyCard1Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard1, enemyCard1Node.transform.localScale, 0.5f);
                LeanTween.move(enemyCard2, enemyCard2Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard2, enemyCard2Node.transform.localScale, 0.5f);
                LeanTween.move(enemyCard3, enemyCard3Node.transform.position, 0.5f);
                LeanTween.scale(enemyCard3, enemyCard3Node.transform.localScale, 0.5f);
            }
        }
    }

    void DrawAnimation()
    {
        if (_playerHand.Count == 0)
        {
            card1Panel.transform.position = deckPanel.transform.position;
            card1Panel.transform.localScale = deckPanel.transform.localScale;
        }

        if (_playerHand.Count == 1)
        {
            card1Panel.transform.position = deckPanel.transform.position;
            card1Panel.transform.localScale = deckPanel.transform.localScale;
            LeanTween.move(card1Panel, card1Node.transform.position, 0.5f);
            LeanTween.scale(card1Panel, card1Node.transform.localScale, 0.5f);
            card2Panel.transform.position = deckPanel.transform.position;
            card2Panel.transform.localScale = deckPanel.transform.localScale;
            card3Panel.transform.position = deckPanel.transform.position;
            card3Panel.transform.localScale = deckPanel.transform.localScale;
        }

        if (_playerHand.Count == 2)
        {
            LeanTween.move(card2Panel, card2Node.transform.position, 0.5f);
            LeanTween.scale(card2Panel, card2Node.transform.localScale, 0.5f);
            card3Panel.transform.position = deckPanel.transform.position;
            card3Panel.transform.localScale = deckPanel.transform.localScale;
        }

        if (_playerHand.Count == 3)
        {
            card3Panel.transform.position = deckPanel.transform.position;
            card3Panel.transform.localScale = deckPanel.transform.localScale;
            LeanTween.move(card3Panel, card3Node.transform.position, 0.5f);
            LeanTween.scale(card3Panel, card3Node.transform.localScale, 0.5f);
        }
    }

    void PlayCard1()
    {
        if (target.targetSelected == true && _cardIsPlayed == false)
        {
            if (_playerHand.Count >= 1)
            {
                AbilityCard playerCard1 = _playerHand.GetCard(0);
                playerCard1.Play();
                //TODO consider expanding Remove to accept a deck position
                _playerHand.Remove(_playerHand.FirstIndex);
                _abilityDiscard.Add(playerCard1);
                Debug.Log("Card added to discard: " + playerCard1.Name);
                target.targetSelected = false;
                _cardIsPlayed = true;
            }
        }
        else
        {
            Debug.Log("No target is selected...");
            Debug.Log("Or you have already played a card this turn");
        }
    }

    void PlayCard2()
    {
        if(target.targetSelected == true && _cardIsPlayed == false) 
        { 
            if(_playerHand.Count >= 2)
            {
                AbilityCard playerCard2 = _playerHand.GetCard(1);
                playerCard2.Play();
                //TODO consider expanding Remove to accept a deck position
                _playerHand.Remove(1);
                _abilityDiscard.Add(playerCard2);
                Debug.Log("Card added to discard: " + playerCard2.Name);
                target.targetSelected = false;
                _cardIsPlayed = true;
            }
        }
        else
        {
            Debug.Log("No target is selected...");
            Debug.Log("Or you have already played a card this turn");
        }
    }

    void PlayCard3()
    {
        if(target.targetSelected == true && _cardIsPlayed == false)
        {
            if (_playerHand.Count == 3)
            {
                AbilityCard playerCard3 = _playerHand.GetCard(2);
                playerCard3.Play();
                //TODO consider expanding Remove to accept a deck position
                _playerHand.Remove(_playerHand.LastIndex);
                _abilityDiscard.Add(playerCard3);
                Debug.Log("Card added to discard: " + playerCard3.Name);
                target.targetSelected = false;
                _cardIsPlayed = true;
            }
        }
        else
        {
            Debug.Log("No target is selected...");
            Debug.Log("Or you have already played a card this turn");
        }
    }
}
