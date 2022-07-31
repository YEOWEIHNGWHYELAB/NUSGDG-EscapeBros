using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class Teleporter : MonoBehaviour
{
    private PhotonView view;
    private bool isMaster;

    void Start()
    {
        view = GetComponent<PhotonView>();
        isMaster = PhotonNetwork.IsMasterClient;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMaster)
        {
            if (collision.name == "Player1(Clone)")
            {
                PlayerReadyController(true, true);
            }
        }
        else
        {
            if (collision.name == "Player2(Clone)")
            {
                PlayerReadyController(false, true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isMaster)
        {
            if (collision.name == "Player1(Clone)")
            {
                PlayerReadyController(true, false);
                // Debug.Log("Player 1 Exit");
            }
        }
        else
        {
            if (collision.name == "Player2(Clone)")
            {
                PlayerReadyController(false, false);
                // Debug.Log("Player 2 Exit");
            }
        }
    }

    public void PlayerReadyController(bool isPlayer1, bool isReady)
    {
        view.RPC("PlayerReadyControllerRPC", RpcTarget.All, isPlayer1, isReady);
    }

    [PunRPC]
    void PlayerReadyControllerRPC(bool isPlayer1, bool isReady)
    {
        if (isPlayer1)
        {
            if (isReady)
            {
                GameManager.playerOneReady = true;
            }
            else
            {
                GameManager.playerTwoReady = false;
            }
        }
        else
        {
            if (isReady)
            {
                GameManager.playerTwoReady = true;
            }
            else
            {
                GameManager.playerTwoReady = false;
            }
        }
    }
}
