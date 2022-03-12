using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    public float xScale;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
        xScale = transform.localScale.x;
    }

    void Update()
    {
        // Prevent accumulation of gravity
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        _anim.SetFloat("AirSpeedY", velocity.y);
        _anim.SetBool("Grounded", controller.collisions.below);

        if (input.x != 0)
        {
            _anim.SetInteger("AnimState", 1);
        }
        else
        {
            _anim.SetInteger("AnimState", 0);
        }

        //Character to face correct direction
        if (input.x > 0) //moving right
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        }
        else if (input.x < 0) //moving left
        {
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180, transform.localRotation.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            _anim.SetTrigger("Jump");
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(
            velocity.x, // Initial velocity
            targetVelocityX, // Final velocity
            ref velocityXSmoothing,
            (controller.collisions.below) // Adjust time to transit
                ? accelerationTimeGrounded // Grounded
                : accelerationTimeAirborne); // Airborne
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
