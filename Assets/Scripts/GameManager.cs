using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static bool playerOneReady;
    public static bool playerTwoReady;
    public static int currLevel = 0;
    private int maxLevel = 2;
    private float resetTime = 1.0F;
    private float timeToReset;
    private PhotonView view;

    private Vector2[] levelStartCoord =
    {
        new Vector2(-4.79f, 0.53f),
        new Vector2(151.61f, 0.53f),
        new Vector2(300.68f, 0.53f)
    };

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        timeToReset = resetTime;
        resetReady();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerOneReady && playerTwoReady)
        {
            if (currLevel >= maxLevel) { currLevel = 0; }
            else { currLevel++; }
            teleportPlayers(currLevel);

            timeToReset -= Time.deltaTime;
            if (timeToReset <= 0)
            {
                resetReady();
                timeToReset = resetTime;
            }   
        }
    }

    void teleportPlayers(int level)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject.Find("Player1(Clone)").transform.position = levelStartCoord[level];
        }
        else
        {
            GameObject.Find("Player2(Clone)").transform.position = levelStartCoord[level] + new Vector2(3f, 0f);
        }
    }

    public void resetReady()
    {
        view.RPC("resetReadyRPC", RpcTarget.All);
    }

    [PunRPC]
    void resetReadyRPC()
    {
        playerOneReady = false;
        playerTwoReady = false;
    }
}
