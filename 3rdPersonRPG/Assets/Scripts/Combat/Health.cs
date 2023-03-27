using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;
    private bool isInvulnerable;

    public event Action onTakeDamage;

    public event Action onDeath;

    public bool isDead => health == 0;

    void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvunerable)
    {
        this.isInvulnerable = isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }

        if(isInvulnerable) { return; }   

        /*health -= damage;

        if(health < 0)
        {health = 0;}
        */ //Shorthand below
        health = Mathf.Max(health - damage, 0);

        onTakeDamage?.Invoke();

        if(health == 0) { onDeath?.Invoke();}

        Debug.Log(health);
    }
}
