using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] playersInGame = new GameObject[2];

    private float distancePlayer1;
    private float distancePlayer2;

    // Start is called before the first frame update
    void Start()
    {
        playersInGame[0] = GameObject.Find("Player1(clone)");
        // playersInGame[1] = GameObject.Find("Player2(clone)");
    }

    // Update is called once per frame
    void Update()
    {
        // playersInGame[0] = GameObject.Find("Player1(clone)");
        // playersInGame[1] = GameObject.Find("Player2(clone)");

        distancePlayer1 = Vector2.Distance(transform.position, playersInGame[0].GetComponent<Transform>().position);
        // distancePlayer2 = Vector2.Distance(transform.position, playersInGame[1].GetComponent<Transform>().position);

        Debug.Log(distancePlayer1);
        // Debug.Log(distancePlayer2);
    }
}
