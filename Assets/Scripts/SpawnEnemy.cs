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
    private int spawnCount = 0;
    private int spawnCountLimit;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        spawnCountLimit = spawnPoints.Length;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2 || spawnCount >= numberEnemy)
        {
            return;
        }

        if (timeBtwSpawns <= 0)
        {
            Vector3 SpawnPosition = spawnPoints[spawnCount].position;
            GameObject currEnemy = PhotonNetwork.Instantiate(enemy.name, SpawnPosition, Quaternion.identity);
            currEnemy.GetComponent<EnemyController>().GetSpawn(spawnPoints[spawnCount].Find("LeftPatrolMax"), spawnPoints[spawnCount].Find("RightPatrolMax"), spawnPoints[spawnCount]);
            spawnCount += 1;

            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
