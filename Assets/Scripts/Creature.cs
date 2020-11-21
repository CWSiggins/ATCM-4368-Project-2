using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour, ITargetable, IDamageable, IHealable
{
    [SerializeField] int _maxHealth;
    public int _currentHealth = 10;
    public int _shield = 10;

    [SerializeField] Text _healthText;
    [SerializeField] Text _shieldText;

    public void Update()
    {
        _healthText.text = ("Opponent Health: " + _currentHealth);
        _shieldText.text = ("Player Defense: " + _shield);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
    }

    public void Kill()
    {
        Debug.Log("Kill the creature!");
        _currentHealth = 0;
        //gameObject.SetActive(false);
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

    public void TakeDamage(int damage)
    {
        if(_shield > 0)
        {
            _shield -= damage;
            if(_shield <= 0)
            {
                _shield = 0;
                Debug.Log("Enemy shield is depleted");
            }
        }
        else
        {
            _currentHealth -= damage;
        }
        Debug.Log("Took damage. Remaining Health:  " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }
    public void Target()
    {
        Debug.Log("Creature has been targeted.");
    }
}
