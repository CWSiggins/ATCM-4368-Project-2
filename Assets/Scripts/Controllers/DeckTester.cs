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

    [SerializeField] GameObject card1Panel;
    [SerializeField] GameObject card2Panel;
    [SerializeField] GameObject card3Panel;
    [SerializeField] Button card1Button;
    [SerializeField] Button card2Button;
    [SerializeField] Button card3Button;
    [SerializeField] Button drawButton;
    [SerializeField] Text deckCount;
    [SerializeField] Text discardCount;

    private void Start()
    {
        SetupAbilityDeck();


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
            PrintPlayerHand();
        }

        deckCount.text = ("" + _abilityDeck.Count);
        discardCount.text = ("" + _abilityDiscard.Count);

        Display();
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

    private void Draw()
    {
        if (_playerHand.Count < 3)
        {
            AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
            Debug.Log("Draw card: " + newCard.Name);
            _playerHand.Add(newCard, DeckPosition.Top);
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

    private void PrintPlayerHand()
    {
        for (int i = 0; i < _playerHand.Count; i++)
        {
            Debug.Log("Player Hand Card: " + _playerHand.GetCard(i).Name);
        }
    }

    void PlayCard1()
    {
        if(_playerHand.Count >= 1)
        {
            AbilityCard playerCard1 = _playerHand.GetCard(0);
            playerCard1.Play();
            //TODO consider expanding Remove to accept a deck position
            _playerHand.Remove(_playerHand.FirstIndex);
            _abilityDiscard.Add(playerCard1);
            Debug.Log("Card added to discard: " + playerCard1.Name);
        }
    }

    void PlayCard2()
    {
        if(_playerHand.Count >= 2)
        {
            AbilityCard playerCard2 = _playerHand.GetCard(1);
            playerCard2.Play();
            //TODO consider expanding Remove to accept a deck position
            _playerHand.Remove(1);
            _abilityDiscard.Add(playerCard2);
            Debug.Log("Card added to discard: " + playerCard2.Name);
        }
    }

    void PlayCard3()
    {
        if(_playerHand.Count == 3)
        {
            AbilityCard playerCard3 = _playerHand.GetCard(2);
            playerCard3.Play();
            //TODO consider expanding Remove to accept a deck position
            _playerHand.Remove(_playerHand.LastIndex);
            _abilityDiscard.Add(playerCard3);
            Debug.Log("Card added to discard: " + playerCard3.Name);
        }
    }
}
