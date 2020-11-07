﻿using System.Collections;
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

    [SerializeField] Button card1Button;
    [SerializeField] Button card2Button;
    [SerializeField] Button card3Button;
    [SerializeField] Button drawButton;
    [SerializeField] Text deckCount;
    [SerializeField] Text discardCount;

    private void Start()
    {
        SetupAbilityDeck();

        if (_playerHand.Count < 3)
        {
            drawButton.onClick.AddListener(Draw);
        }

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

    private void Draw()
    {
        AbilityCard newCard = _abilityDeck.Draw(DeckPosition.Top);
        Debug.Log("Draw card: " + newCard.Name);
        _playerHand.Add(newCard, DeckPosition.Top);

        if(_playerHand.Count > 0)
        {
            AbilityCard playerCard1 = _playerHand.GetCard(0);
            _abilityCardView.Display1(playerCard1);
        }

        if (_playerHand.Count > 1)
        {
            AbilityCard playerCard2 = _playerHand.GetCard(1);
            _abilityCardView.Display2(playerCard2);
        }

        if (_playerHand.Count > 2)
        {
            AbilityCard playerCard3 = _playerHand.GetCard(2);
            _abilityCardView.Display3(playerCard3);
        }

        if (_abilityDeck.Count == 0)
        {
            foreach(AbilityCardData abilityData in _abilityDeckConfig)
            {
                AbilityCard reAddAbilityCard = new AbilityCard(abilityData);
                _abilityDeck.Add(reAddAbilityCard);
                _abilityDiscard.Remove(_abilityDiscard.Count);
            }
            _abilityDeck.Shuffle();
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
            _playerHand.Remove(_playerHand.MiddleIndex);
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
