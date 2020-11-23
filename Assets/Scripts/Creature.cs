using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour, ITargetable, IDamageable, IHealable
{
    public int _currentHealth = 10;
    public int _shield = 10;

    [SerializeField] Text _healthText;
    [SerializeField] Text _shieldText;

    public void Start()
    {
        _shieldText.color = Color.blue;
        _healthText.color = Color.green;
    }

    public void Update()
    {
        _healthText.text = ("Opponent Health: " + _currentHealth);
        _shieldText.text = ("Opponent Defense: " + _shield.ToString());
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
    }

    public void Kill()
    {
        Debug.Log("Kill the creature!");
        _currentHealth = 0;
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
            if (damage > _shield)
            {
                int damageLeft = damage - _shield;
                _shield -= damage;
                _currentHealth -= damageLeft;
                HealthDamageFeedback();
            }
            _shield -= damage;
            ShieldDamageFeedback();
            if(_shield <= 0)
            {
                _shield = 0;
                Debug.Log("Enemy shield is depleted");
            }
        }
        else
        {
            _currentHealth -= damage;
            HealthDamageFeedback();
        }
        Debug.Log("Took damage. Remaining Health:  " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    private void HealthDamageFeedback()
    {
        StartCoroutine("HealthDamage");
    }

    private void ShieldDamageFeedback()
    {
        StartCoroutine("ShieldDamage");
    }
    public void Target()
    {
        Debug.Log("Creature has been targeted.");
    }

    IEnumerator ShieldDamage()
    {
        LeanTween.colorText(_shieldText.rectTransform, Color.red, 0.5f);
        LeanTween.scale(_shieldText.rectTransform, new Vector3(0.9f, 0.9f, 0.9f), 0.5f);
        yield return new WaitForSeconds(1);
        LeanTween.colorText(_shieldText.rectTransform, Color.blue, 0.5f);
        LeanTween.scale(_shieldText.rectTransform, new Vector3(0.75f, 0.75f, 0.75f), 0.5f);
    }
    IEnumerator HealthDamage()
    {
        LeanTween.colorText(_healthText.rectTransform, Color.red, 0.5f);
        LeanTween.scale(_healthText.rectTransform, new Vector3(0.9f, 0.9f, 0.9f), 0.5f);
        yield return new WaitForSeconds(1);
        LeanTween.colorText(_healthText.rectTransform, Color.green, 0.5f);
        LeanTween.scale(_healthText.rectTransform, new Vector3(0.75f, 0.75f, 0.75f), 0.5f);
    }
}
