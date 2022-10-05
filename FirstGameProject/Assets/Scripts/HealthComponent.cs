using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    private float health;
    public float maxHealth = 150f;
    public event Action OnPlayerDeath;
    ParticleSystem particlesystem;
    GameObject PlayerObj;
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        particlesystem = GetComponent<ParticleSystem>();
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
        
        healthbar.SetHealth(health);
   
    }

        public void DamageEnemy(float damage)
    {
        health -= damage;
        particlesystem.Play();
        Debug.Log(health);
    }

 
}
