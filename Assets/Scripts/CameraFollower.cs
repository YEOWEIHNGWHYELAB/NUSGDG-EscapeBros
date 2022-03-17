using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CameraFollower : MonoBehaviour
{
    private GameObject player;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Awake()
    {
        // player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                player = GameObject.Find("Player1(Clone)");
            } else
            {
                player = GameObject.Find("Player2(Clone)");
            }
        }
        
        // Temporary vector
        Vector3 temp = player.transform.position;
        temp.z = transform.position.z;

        // Assign value to Camera position
        transform.position = temp;
    }
}
