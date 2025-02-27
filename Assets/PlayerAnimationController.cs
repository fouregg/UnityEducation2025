using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator animator;
    private MovementController movementController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerState prevState;
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
            case (PlayerState.Runing):
                if (prevState != PlayerState.Runing)
                    animator.SetTrigger("isRun");
                break;
            case (PlayerState.Idle):
                if (prevState != PlayerState.Idle)
                    animator.SetTrigger("isIdle");
                break;
            case (PlayerState.Jumping):
                if (prevState != PlayerState.Jumping)
                    animator.SetTrigger("isJump");
                animator.SetFloat("jumpSpeed", movementController.Rb.linearVelocityY);
                break;
        }
        prevState = movementController.State;
    }
}
