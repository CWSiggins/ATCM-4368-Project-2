using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] Text _nameTextUI1 = null;
    [SerializeField] Text _costTextUI1 = null;
    [SerializeField] Image _graphicUI1 = null;

    [SerializeField] Text _nameTextUI2 = null;
    [SerializeField] Text _costTextUI2 = null;
    [SerializeField] Image _graphicUI2 = null;

    [SerializeField] Text _nameTextUI3 = null;
    [SerializeField] Text _costTextUI3 = null;
    [SerializeField] Image _graphicUI3 = null;

    public void Display1(AbilityCard abilityCard)
    {
        _nameTextUI1.text = abilityCard.Name;
        _costTextUI1.text = abilityCard.Cost.ToString();
        _graphicUI1.sprite = abilityCard.Graphic;
    }

    public void Display2(AbilityCard abilityCard)
    {
        _nameTextUI2.text = abilityCard.Name;
        _costTextUI2.text = abilityCard.Cost.ToString();
        _graphicUI2.sprite = abilityCard.Graphic;
    }

    public void Display3(AbilityCard abilityCard)
    {
        _nameTextUI3.text = abilityCard.Name;
        _costTextUI3.text = abilityCard.Cost.ToString();
        _graphicUI3.sprite = abilityCard.Graphic;
    }
}
