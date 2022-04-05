using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player1(Clone)")
        {
            GameManager.playerOneReady = true;
        }
        if (collision.name == "Player2(Clone)")
        {
            GameManager.playerTwoReady = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player1(Clone)")
        {
            GameManager.playerOneReady = false;
        }
        if (collision.name == "Player2(Clone)")
        {
            GameManager.playerTwoReady = false;
        }
    }
}
