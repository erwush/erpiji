using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public EnemyAttribute attr;

    public float health;
    public FloatingHealthBar healthBar;
    public EnemyWave waveScr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = attr.maxHealth;


    }

    void Update()
    {
        healthBar.UpdateHealthBar(health, attr.maxHealth);
    }



    public void HealthChange(float amount)
    {
        health += amount;
        if (health > attr.maxHealth)
        {
            health = attr.maxHealth;
        }
        else if (health <= 0)
        {
            Die();
            Destroy(healthBar);
        }


    }
    
    public void Die()
    {
        waveScr.EnemyDied(gameObject);
        Destroy(gameObject);
    }
}
