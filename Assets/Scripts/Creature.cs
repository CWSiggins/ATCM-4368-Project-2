using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour, ITargetable, IDamageable
{
    public int _currentHealth = 10;

    [SerializeField] Text _healthText;

    public void Update()
    {
        _healthText.text = ("Opponent Health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
    }

    public void Kill()
    {
        Debug.Log("Kill the creature!");
        //gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
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
