using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]
    int currentHealth;
    PhotonView view;
    public EnemyHealthBar enemyHealthbar;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        InitializeEnemyHealth();
    }

    public void InitializeEnemyHealth()
    {
        currentHealth = maxHealth;
        enemyHealthbar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        view.RPC("TakeDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    public void TakeDamageRPC(int damage)
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
        Destroy(gameObject);
    }

    void Update()
    {
        enemyHealthbar.SetHealth(currentHealth, maxHealth);
    }
}
