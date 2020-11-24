using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] Text _nameTextUI1 = null;
    [SerializeField] Text _typeTextUI1 = null;
    [SerializeField] Text _amountTextUI1 = null;
    [SerializeField] Image _graphicUI1 = null;

    [SerializeField] Text _nameTextUI2 = null;
    [SerializeField] Text _typeTextUI2 = null;
    [SerializeField] Text _amountTextUI2 = null;
    [SerializeField] Image _graphicUI2 = null;

    [SerializeField] Text _nameTextUI3 = null;
    [SerializeField] Text _typeTextUI3 = null;
    [SerializeField] Text _amountTextUI3 = null;
    [SerializeField] Image _graphicUI3 = null;

    public void Display1(AbilityCard abilityCard)
    {
        _nameTextUI1.text = abilityCard.Name;
        _typeTextUI1.text = abilityCard.Type;
        _graphicUI1.sprite = abilityCard.Graphic;
        if(abilityCard.Type == "Attack")
        {
            _amountTextUI1.text = "-" + abilityCard.Amount;
        }
        else
        {
            _amountTextUI1.text = "+" + abilityCard.Amount;
        }
    }

    public void Display2(AbilityCard abilityCard)
    {
        _nameTextUI2.text = abilityCard.Name;
        _typeTextUI2.text = abilityCard.Type;
        _graphicUI2.sprite = abilityCard.Graphic;
        if (abilityCard.Type == "Attack")
        {
            _amountTextUI2.text = "-" + abilityCard.Amount;
        }
        else
        {
            _amountTextUI2.text = "+" + abilityCard.Amount;
        }
    }

    public void Display3(AbilityCard abilityCard)
    {
        _nameTextUI3.text = abilityCard.Name;
        _typeTextUI3.text = abilityCard.Type;
        _graphicUI3.sprite = abilityCard.Graphic;
        if (abilityCard.Type == "Attack")
        {
            _amountTextUI3.text = "-" + abilityCard.Amount;
        }
        else
        {
            _amountTextUI3.text = "+" + abilityCard.Amount;
        }
    }
}
