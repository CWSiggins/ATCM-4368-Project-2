using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, ITargetable, IHealable
{
    [SerializeField] int _maxHealth;
    public int _currentHealth;
    public int _shield;

    [SerializeField] Text _healthText;
    [SerializeField] Text _shieldText;
    [SerializeField] Button _damageButton;

    public void Start()
    {
        _damageButton.onClick.AddListener(Kill);
    }

    public void Update()
    {
        _healthText.text = ("Player Health: " + _currentHealth);
        _shieldText.text = ("Player Defense: " + _shield);
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        Debug.Log("Healed. Remaining Health:  " + _currentHealth);
        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Shield(int amount)
    {
        _shield += amount;
    }

    public void Target()
    {
        Debug.Log("Player has been targeted.");
    }

    private void Kill()
    {
        _currentHealth = 0;
    }
}
