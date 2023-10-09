using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 100.0f)] private float _startingHealth = 10.0f;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _isFriendly;

    public UnityEvent onDeath;

    private void OnEnable()
    {
        _currentHealth = _startingHealth;
    }

    public void DealDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0.0f)
        {
            onDeath.Invoke();
        }
    }

    public void AutoKill()
    {
        DealDamage(_currentHealth);
    }

    public bool IsFriendly() { return _isFriendly; }
}
