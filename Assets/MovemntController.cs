using UnityEngine;

public class MovemntController : MonoBehaviour
{
    [SerializeField] private float speedMove = 20f;
    [SerializeField] private float speedJump = 10f;
    private bool jump = true;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // take component from object 
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.linearVelocityX = Input.GetAxis("Horizontal") * speedMove;
        }
        
        if (jump && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, speedJump), ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!jump)
        { 
            jump = true;
        }
    }
}
