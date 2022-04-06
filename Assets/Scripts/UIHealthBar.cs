using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UIHealthBar : MonoBehaviour
{
    public Slider slider;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        view.RPC("SetMaxHealthRPC", RpcTarget.All, maxHealth);
    }

    public void SetHealth(int health)
    {
        view.RPC("SetHealthRPC", RpcTarget.All, health);
    }

    [PunRPC]
    public void SetMaxHealthRPC(int maxHealth)
    {
        slider.maxValue = maxHealth * 1f;
        slider.value = maxHealth * 1f;
    }

    [PunRPC]
    public void SetHealthRPC(int health)
    {
        slider.value = health * 1f;
    }
}
