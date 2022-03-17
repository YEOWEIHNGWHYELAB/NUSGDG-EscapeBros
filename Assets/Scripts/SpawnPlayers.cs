using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    private bool isMaster;
    public float minX, minY, maxX, maxY;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isMaster = true;
        }
        else
        {
            isMaster = false;
        }

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (isMaster)
        {
            PhotonNetwork.Instantiate("Player1", randomPosition, Quaternion.identity); // Specify name of game object inside resources folder you wanna spawn
        } else
        {
            PhotonNetwork.Instantiate("Player2", randomPosition, Quaternion.identity);
        }
    }
}
