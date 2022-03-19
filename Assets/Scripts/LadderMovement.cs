using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float climbSpeed;
    private bool isLadder;
    private bool isCimbling;

    Vector2 velocity;

    private Rigidbody2D rb;
    Controller2D controller;
    Player playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller2D>();
        playerMovement = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isCimbling = true;
        }
    }

    private void FixedUpdate()
    {
        if (isCimbling)
        {
            playerMovement.DisableGravity();
            velocity = new Vector2(velocity.x, vertical * climbSpeed);
            controller.Move(velocity * Time.deltaTime);
        } else
        {
            playerMovement.EnableGravity();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            //Debug.Log("Hello");
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            isLadder = false;
            isCimbling = false;
        }
    }
}
