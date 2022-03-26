using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float climbSpeed;
    private bool isLadder;
    private bool isCimbling;

    Vector2 velocity;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical"), 0.1f);

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isCimbling = true;
        }
    }

    private void FixedUpdate()
    {
        if (isCimbling)
        {
            rb.velocity = new Vector2(velocity.x, vertical * climbSpeed);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
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
