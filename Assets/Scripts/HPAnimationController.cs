using UnityEngine;

public class HPAnimationController : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DestroyAnim()
    {
        animator.SetTrigger("Destroy");
    }
}
