using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currHealth;

    private Animator _anim;
    private bool isMaster;

    public GameObject myBar;
    public GameObject broBar;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isMaster = true;
        } else
        {
            isMaster = false;
        }

        view = GetComponent<PhotonView>();
        _anim = GetComponent<Animator>();
        _currHealth = _maxHealth;

        // Health Bar Objects
        myBar = GameObject.Find("Health");
        broBar = GameObject.Find("HealthBro");
        myBar.GetComponent<UIHealthBar>().SetMaxHealth(_maxHealth);
        broBar.GetComponent<UIHealthBar>().SetMaxHealth(_maxHealth);
        // Debug.Log("Health initialized!");
    }
    
    private void TakeDamage(int damage, bool isMaster)
    {
        if (isMaster)
        {
            _currHealth -= damage;
            myBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            _anim.SetTrigger("Hurt");

            if (_currHealth <= 0)
            {
                myBar.GetComponent<UIHealthBar>().SetHealth(0);
                Die();
            }
        } else
        {
            _currHealth -= damage;
            broBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            _anim.SetTrigger("Hurt");

            if (_currHealth <= 0)
            {
                broBar.GetComponent<UIHealthBar>().SetHealth(0);
                Die();
            }
        }
    }

    private void Die()
    {
        _anim.SetTrigger("Death");
        Debug.Log("Player is dead!");
    }

    private void Revive(bool isMaster)
    {
        if (isMaster)
        {
            _currHealth = _maxHealth;
            myBar.GetComponent<UIHealthBar>().SetHealth(_maxHealth);
            _anim.Rebind();
            _anim.Update(0f);
        } else
        {
            _currHealth = _maxHealth;
            broBar.GetComponent<UIHealthBar>().SetHealth(_maxHealth);
            _anim.Rebind();
            _anim.Update(0f);
        }
        
    }

    public void HealthControl(bool takeDamage, bool isMaster, int healthDelta)
    {
        if (isMaster)
        {
            if (takeDamage)
            {
                // _currHealth -= healthDelta;
                // myBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
                TakeDamage(healthDelta, isMaster);
            }
            else
            {
                // _currHealth += healthDelta;
                // myBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
                Revive(isMaster);
            }
        }
        else
        {
            if (takeDamage)
            {
                // _currHealth -= healthDelta;
                // broBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
                TakeDamage(healthDelta, isMaster);
            }
            else
            {
                // _currHealth += healthDelta;
                // broBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
                Revive(isMaster);
            }
        }
    }
    private void HealthTest() {
        if (isMaster)
        {
            if (Input.GetKeyDown("o"))
            {
                _currHealth -= 20;
                myBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            }
            if (Input.GetKeyDown("p"))
            {
                _currHealth += 20;
                myBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            }
        } else 
        {
            if (Input.GetKeyDown("o"))
            {
                _currHealth -= 20;
                broBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            }
            if (Input.GetKeyDown("p"))
            {
                _currHealth += 20;
                broBar.GetComponent<UIHealthBar>().SetHealth(_currHealth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            HealthTest();
        }
    }
}
