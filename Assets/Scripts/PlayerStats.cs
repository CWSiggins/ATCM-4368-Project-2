using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, ITargetable, IHealable, IDamageable
{
    public int _currentHealth;
    public int _shield;

    [SerializeField] Text _healthText;
    [SerializeField] Text _shieldText;

    public void Update()
    {
        _healthText.text = ("Player Health: " + _currentHealth);
        _shieldText.text = ("Player Defense: " + _shield);
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        Debug.Log("Healed. Remaining Health:  " + _currentHealth);
    }

    public void Shield(int amount)
    {
        _shield += amount;
    }

    public void TakeDamage(int damage)
    {
        if(_shield > 0)
        {
            _shield -= damage;
            if(_shield <= 0)
            {
                _shield = 0;
                Debug.Log("Your shield is depleted");
            }
        }
        else
        {
            _currentHealth -= damage;
            Debug.Log("Took damage. Remaining Health:  " + _currentHealth);
        }
        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Target()
    {
        Debug.Log("Player has been targeted.");
    }

    public void Kill()
    {
        _currentHealth = 0;
    }
}
