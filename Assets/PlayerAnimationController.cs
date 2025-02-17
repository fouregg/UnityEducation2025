using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private MovementController movementController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (movementController.State) 
        {
            case (MovementController.PlayerState.Idle):
                animator.SetTrigger("isIdle");
                break;
            case (MovementController.PlayerState.Runing):
                animator.SetTrigger("isRun");
                break;
            case (MovementController.PlayerState.Jumping):
                animator.SetTrigger("isJump");
                Debug.Log(movementController.Rb.linearVelocityY);
                animator.SetFloat("jumpSpeed", movementController.Rb.linearVelocityY);
                break;
        }

    }
}
