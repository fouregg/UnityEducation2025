using UnityEngine;

public class MovementController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,    // Простаивание
        Jumping, // Прыжок
        Runing,  // Бег
        Hiting   // Урон
    }

    [SerializeField] private float speedMove = 20f;
    [SerializeField] private float speedJump = 10f;
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

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            rb.linearVelocityX = Input.GetAxis("Horizontal") * speedMove;
            if(state == PlayerState.Idle)
                state = PlayerState.Runing;

            if (
                (Input.GetAxis("Horizontal") > 0 && !lookRight) || 
                (Input.GetAxis("Horizontal") < 0 && lookRight)
               )
                flip();
        }
        else if (state != PlayerState.Jumping && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, speedJump), ForceMode2D.Impulse);
            state = PlayerState.Jumping;
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.1 && state == PlayerState.Runing)
        {
            state = PlayerState.Idle;
        }

        Debug.Log(state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == PlayerState.Jumping)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.1)
                state = PlayerState.Runing;
            else
                state = PlayerState.Idle;
        }
    }

    private void flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookRight = !lookRight;
    }
}
