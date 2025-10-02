using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public Action<float> OnHealthChanged;
    public Action OnDamageTaken;

    [SerializeField] private float baseHealth;
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public bool dead { get; private set; }
    public GameObject user;

    public void Awake()
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
    }

    public void Init()
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
        dead = false;
    }

    public void Damage(HitInfo hitInfo)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth - hitInfo.damage, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
        OnDamageTaken?.Invoke();
        if (currentHealth <= 0)
        {
            dead = true;
        }
    }
}
