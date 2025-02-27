using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speedMove = 20f;
    [SerializeField] private float speedJump = 10f;
    [SerializeField] private float rayDistance = 1.1f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private PlayerState state;
    private bool lookRight;

    public PlayerState State { get => state; set => state = value; } // Геттер и сеттер в С#
    public Rigidbody2D Rb { get => rb; set => rb = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // take component from object 
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.Idle;
        lookRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        /* difficult code
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speedMove * Time.deltaTime, transform.position.y, transform.position.z);
        }
        */
        // transfrom - object of component Transform in Unity, Input.GetAxis(name Axis) - method for get keyInput on keyboard
        // not correct work for wall
        // transform.Translate(new Vector2(Input.GetAxis("Horizontal"), 0) * speedMove * Time.deltaTime);
        //rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speedMove, 0));

        // runing logic
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
           Run();
        }
        // idle logic
        else if (!Input.anyKey && state != PlayerState.Jumping)
        {
            Idle();
        }
        // jumpic logic
        if (state != PlayerState.Jumping && Input.GetKey(KeyCode.Space))
        {
           Jump();
        }

        if (IsGrounded() && rb.linearVelocityY <= 0.1)
        {
            if (!Input.anyKey)
                state = PlayerState.Idle;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                state = PlayerState.Runing;
        }
    }


    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookRight = !lookRight;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector3.down, rayDistance, groundLayer); 
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, speedJump), ForceMode2D.Impulse);
        state = PlayerState.Jumping;
    }

    private void Run()
    {
        rb.linearVelocityX = Input.GetAxis("Horizontal") * speedMove;
        if (state != PlayerState.Jumping)
            state = PlayerState.Runing;
        if (
            (Input.GetAxis("Horizontal") > 0 && !lookRight) ||
            (Input.GetAxis("Horizontal") < 0 && lookRight)
           )
            Flip();
    }

    private void Idle()
    {
        rb.linearVelocityX = 0;
        state = PlayerState.Idle;
    }

}
