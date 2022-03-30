using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private const float BOXCAST_ANGLE = 0;
    private const float BOXCAST_DISTANCE = 0.03f;

    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    PhotonView view;
    Animator _anim;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        _anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                GetComponent<PhotonTransformView>().enabled = false;
            }

            float input = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(input * speed, rb.velocity.y);
            if (input > 0) //moving right
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (input < 0) //moving left
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            UpdateAnimation(input);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            BOXCAST_ANGLE,
            Vector2.down,
            BOXCAST_DISTANCE,
            groundLayer);
        bool isOnGround = raycastHit.collider != null;
        return isOnGround;
    }

    private void UpdateAnimation(float input) {
        _anim.SetFloat("AirSpeedY", rb.velocity.y);
        _anim.SetBool("Grounded", IsGrounded());
        if (input != 0)
        {
            _anim.SetInteger("AnimState", 1);
            //_anim.SetBool("isRunning", false);
        }
        else
        {
            _anim.SetInteger("AnimState", 0);
            //_anim.SetBool("isRunning", true);
        }
    }
}


