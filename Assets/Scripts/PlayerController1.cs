using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerController1 : MonoBehaviour
{
    //OLD
    public float speed = 5f;
    public float jumpForce = 3f;
    public float playerFeetRadius = 0.2f;
    private float _direction = 0f;
    private bool _isGrounded = false;
    //OLD

    public bool isMultiplayer = false;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    public Transform playerFeet;
    public LayerMask groundLayer;
    private Rigidbody2D _playerRb;
    private Animator _anim;
  
    // Start is called before the first frame update
    void Start()
    {
        //Get reference to rigidbody component for left right movement and jumping
        //_playerRb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMultiplayer)
        {
            PlayerMove();
            PlayerJump();
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            //Multiplayer code here
        }
           
    }

    private void PlayerMove()
    {
        //Get direction keypress from user
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        _direction = input.x;
        _anim.SetFloat("AirSpeedY", velocity.y);

        float targetVelocityX = input.x * moveSpeed;

        velocity.x = Mathf.SmoothDamp(
            velocity.x, // Initial velocity
            targetVelocityX, // Final velocity
            ref velocityXSmoothing,
            (controller.collisions.below) // Adjust time to transit
                ? accelerationTimeGrounded // Grounded
                : accelerationTimeAirborne); // Airborne;

        //Move the player
        if (_direction != 0)
        {
            _anim.SetInteger("AnimState", 1);
        }
        else
        {
            _anim.SetInteger("AnimState", 0);
        }

        //Character to face correct direction
        if (_direction > 0) //moving right
        {
            transform.localScale = new Vector3(0.9f, transform.localScale.y, transform.localScale.z);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        }
        else if (_direction < 0) //moving left
        {
            transform.localScale = new Vector3(-0.9f, transform.localScale.y, transform.localScale.z);
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180, transform.localRotation.z);
        }
    }

    private void PlayerJump()
    {
        //Check if player is grounded
        _anim.SetBool("Grounded", controller.collisions.below);

        //Handle player jumping, player jumps when jump key is pressed and its not midair
        //if (Input.GetButtonDown("Jump") && _isGrounded)
        //{
        //    _anim.SetTrigger("Jump");
        //    _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce);
        //}

        if (Input.GetButtonDown("Jump") && controller.collisions.below)
        {
            _anim.SetTrigger("Jump");
            velocity.y = jumpVelocity;
        }

        //multiplayer has issues with rigidbody
        //control directly using transform of gameObject (its rework time)
    }

}
