using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private Entity _owner;
    private float _maxHealth;
    private float _currentHealth;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private UnityEvent _deadEvent;
    public void Initialized(float value)
    {
        _maxHealth = value;
        _currentHealth = _maxHealth;
        _text.SetText(_currentHealth.ToString());
    }

    public float GetCurrentHealth() => _currentHealth;
    public void SetCurrentHelath(int value)
    {
        _currentHealth = value;
        _text.SetText(_currentHealth.ToString());
    }

    public void ApplyeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);
        _text.SetText(_currentHealth.ToString());
        if (_currentHealth <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<Health>(out Health health))
            {
                float enemtHealth = health.GetCurrentHealth();
                health.ApplyeDamage(_currentHealth);
                _currentHealth = Mathf.Max(_currentHealth - enemtHealth, 0);
                _text.SetText(_currentHealth.ToString());
                if (_currentHealth <= 0)
                {
                    Dead();
                    return;
                }
            }

        }
    }
    public void Dead()
    {
        _deadEvent?.Invoke();
        Destroy(gameObject);
    }
}
