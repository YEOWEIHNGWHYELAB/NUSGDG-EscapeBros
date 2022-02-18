using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currHealth;

    public UIHealthBar uiHealthBar;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _currHealth = _maxHealth;
        uiHealthBar = FindObjectOfType<UIHealthBar>();
        uiHealthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _currHealth -= damage;
        uiHealthBar.SetHealth(_currHealth);
        _anim.SetTrigger("Hurt");

        if (_currHealth <= 0)
        {
            uiHealthBar.SetHealth(0);
            Die();
        }
    }
    private void Die()
    {
        _anim.SetTrigger("Death");
        Debug.Log("Player is dead!");
    }

    private void Revive()
    {
        _currHealth = _maxHealth;
        uiHealthBar.SetMaxHealth(_maxHealth);
        _anim.Rebind();
        _anim.Update(0f);
    }

    private void HealthTest() {
        if (Input.GetKeyDown("o"))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown("p"))
        {
            Revive();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthTest();
    }
}
