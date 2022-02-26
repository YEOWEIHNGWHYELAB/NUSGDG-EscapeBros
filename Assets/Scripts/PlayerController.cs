using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 3f;
    public float playerFeetRadius = 0.2f;
    private float _direction = 0f;
    private bool _isGrounded = false;

    public bool isMultiplayer = false;

    public Transform playerFeet;
    public LayerMask groundLayer;
    private Rigidbody2D _playerRb;
    private Animator _anim;
  
    // Start is called before the first frame update
    void Start()
    {
        //Get reference to rigidbody component for left right movement and jumping
        _playerRb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMultiplayer)
        {
            PlayerMove();
            PlayerJump();
        }
        else
        {
            //Multiplayer code here
        }
           
    }

    private void PlayerMove()
    {
        //Get direction keypress from user
        _direction = Input.GetAxis("Horizontal");
        _anim.SetFloat("AirSpeedY", _playerRb.velocity.y);

        //Move the player
        if (_direction != 0)
        {
            _playerRb.velocity = new Vector2(_direction * speed, _playerRb.velocity.y);
            _anim.SetInteger("AnimState", 1);
        }
        else
        {
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);
            _anim.SetInteger("AnimState", 0);
        }

        //Character to face correct direction
        if (_direction > 0) //moving right
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        }
        else if (_direction < 0) //moving left
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180, transform.localRotation.z);
        }
    }

    private void PlayerJump()
    {
        //Check if player is grounded
        _isGrounded = Physics2D.OverlapCircle(playerFeet.position, playerFeetRadius, groundLayer);
        _anim.SetBool("Grounded", _isGrounded);

        //Handle player jumping, player jumps when jump key is pressed and its not midair
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _anim.SetTrigger("Jump");
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce);
        }

        //multiplayer has issues with rigidbody
        //control directly using transform of gameObject (its rework time)
    }

}
