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
    private int _attackType = 1;
    private float _attackAnimDelayTime = 0.4f;
    private float _attackAnimDelayTimer;
    private float _attackAnimResetTime = 1f;
    private float _attackAnimResetTimer;

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
            PlayerAttack();
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
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (_direction < 0) //moving left
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
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

    private void PlayerAttack()
    {
        _attackAnimDelayTimer -= Time.deltaTime;
        _attackAnimResetTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire3") && _attackAnimDelayTimer < 0f)
        {
            _anim.SetTrigger("Attack" + _attackType.ToString());
            _attackAnimDelayTimer = _attackAnimDelayTime;
            _attackAnimResetTimer = _attackAnimResetTime;
            if (_attackType < 3) _attackType++;
            else _attackType = 1;
        } 

        if (_attackAnimResetTimer < 0f)
        {
            _attackAnimResetTimer = _attackAnimResetTime;
            _attackType = 1;
        }
    }
}
