using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstratHealth : MonoBehaviour
{
    [SerializeField] private float _health;

    public float Health => _health;

    internal void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_health < 0)
            return;

        _health -= damage;

        if (_health < 0)
        {
            _health = 0;
            Die();
        }
    }

    internal abstract void Die();
}
