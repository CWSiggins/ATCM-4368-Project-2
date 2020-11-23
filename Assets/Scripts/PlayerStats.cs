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

    public void Start()
    {
        _shieldText.color = Color.blue;
        _healthText.color = Color.green;
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
            if (_shield <= 0)
            {
                _shield = 0;
                Debug.Log("Your shield is depleted");
            }
        }
        else
        {
            _currentHealth -= damage;
            HealthDamageFeedback();
        }
        if (_currentHealth <= 0)
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
        Debug.Log("Player has been targeted.");
    }

    public void Kill()
    {
        _currentHealth = 0;
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
