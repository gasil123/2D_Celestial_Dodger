using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] float parachuteOpenYAxisValue = 3;
    [SerializeField] float parachuteOpenGravityScale= 0.2f;

    [SerializeField] Animator animator;
    [SerializeField] Transform foot;
    [SerializeField] Rigidbody2D _rb;

    bool isGrounded = false;
    public void Die()
    {
        animator?.SetBool("Death", true);
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        IdleAnimation();
        JumpAnimation();
        _rb.centerOfMass = foot.localPosition;
    }
    private void IdleAnimation()
    {
        if (IsGrounded())
        {
            animator.SetBool("Jump", false);
            animator?.SetBool("Idle", true);
        }
    }
    private void JumpAnimation()
    {
        if(transform.position.y < parachuteOpenYAxisValue && !isGrounded)
        {
            _rb.gravityScale = parachuteOpenGravityScale;
            animator.SetBool("Jump", true);
            animator?.SetBool("Idle", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private bool IsGrounded()
    {
        return isGrounded;
    }
}
