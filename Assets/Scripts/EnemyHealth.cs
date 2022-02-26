using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]
    int currentHealth;

    public EnemyHealthBar enemyHealthbar;
    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemyHealth();
    }

    public void InitializeEnemyHealth()
    {
        currentHealth = maxHealth;
        enemyHealthbar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHealthbar.SetHealth(currentHealth, maxHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    void Update()
    {
        enemyHealthbar.SetHealth(currentHealth, maxHealth);
    }
}
