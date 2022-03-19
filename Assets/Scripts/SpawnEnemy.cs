using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float startTimeBtwSpawns;
    float timeBtwSpawns;
    public int numberEnemy;
    private int currentNumberEnemy;
    private int spawnCount = 0;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2 || currentNumberEnemy >= numberEnemy || spawnCount > 17)
        {
            return;
        }

        if (timeBtwSpawns <= 0)
        {
            spawnCount += 1;

            Vector3 SpawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, SpawnPosition, Quaternion.identity);
            currentNumberEnemy += 1;

            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
