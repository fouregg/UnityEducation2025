using UnityEngine;

public class PlayerCheckpointController : MonoBehaviour
{
    private GameObject lastCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastCheckpoint = GameObject.Find("Checkpoint1");
        Animator animator = lastCheckpoint.GetComponent<Animator>();
        animator.SetTrigger("enable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            if (collision.gameObject != lastCheckpoint)
            {
                Animator animator = lastCheckpoint.GetComponent<Animator>();
                animator.SetTrigger("disable");
                lastCheckpoint = collision.gameObject;
                animator = lastCheckpoint.GetComponent<Animator>();
                animator.SetTrigger("enable");
            }
        }

        if (collision.gameObject.name == "DeadZone")
            gameObject.transform.position = lastCheckpoint.transform.position + Vector3.up;
    }
}
