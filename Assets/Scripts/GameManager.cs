using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static bool playerOneReady = false;
    public static bool playerTwoReady = false;
    public static int currLevel = 0;
    private float teleTimeDelay = 0.2f;
    private float teleDelay;
    private float levelTimeDelay = 1f;
    private float levelDelay;
    private bool isMaster;
    private bool hasIncremented = false;
    private bool hasTeleported = false;
    private int maxLevel = 2;
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
        teleDelay = teleTimeDelay;
        levelDelay = levelTimeDelay;
        view = GetComponent<PhotonView>();
        isMaster = PhotonNetwork.IsMasterClient;
        resetReady();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOneReady && playerTwoReady)
        {
            // Debug.Log("Both Ready to Teleport!");

            if (hasTeleported)
            {
                teleDelay -= Time.deltaTime;
            }

            if (teleDelay < 0f && hasTeleported)
            {
                teleDelay = teleTimeDelay;
                resetReady();
            }

            if (isMaster && !hasIncremented)
            {
                hasIncremented = true;
                currentLevel();
            }

            levelDelay -= Time.deltaTime;

            if (levelDelay < 0f)
            {
                teleportPlayers(currLevel);
                levelDelay = levelTimeDelay;
                hasIncremented = false;
            }
        }
    }

    void teleportPlayers(int level)
    {
        // GameObject player1 = GameObject.Find("Player1(Clone)");
        // GameObject player2 = GameObject.Find("Player2(Clone)");

        if (isMaster)
        {
            GameObject.Find("Player1(Clone)").transform.position = levelStartCoord[level];
            // Debug.Log("Player 1 Teleported");
        }
        else
        {
            GameObject.Find("Player2(Clone)").transform.position = levelStartCoord[level] + new Vector2(3f, 0f);
            // Debug.Log("Player 2 Teleported");
        }

        hasTeleported = true;
    }

    public void currentLevel()
    {
        view.RPC("currentLevelRPC", RpcTarget.All);
    }

    [PunRPC]
    public void currentLevelRPC()
    {
        if (currLevel >= maxLevel) { currLevel = 0; }
        else { currLevel++; }
    }

    public void resetReady()
    {
        hasTeleported = false;
        view.RPC("resetReadyRPC", RpcTarget.All);
    }

    [PunRPC]
    void resetReadyRPC()
    {
        playerOneReady = false;
        playerTwoReady = false;
    }
}
