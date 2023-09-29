using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] float parachuteOpenYAxisValue = 3;
    [SerializeField] float parachuteOpenGravityScale= 0.1f;
    [SerializeField] float initialGravityScale= 0.8f;

    [SerializeField] Animator animator;
    [SerializeField] Transform foot;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] GameObject blasrObject;
    Animator blast;
    bool isGrounded = false;
    int deathAnimId;
    int jumpAnimId;
    int idleAnimId;
    private void Start()
    {
        blast = blasrObject.GetComponent<Animator>();
        deathAnimId = Animator.StringToHash("Death");
        jumpAnimId = Animator.StringToHash("Jump");
        idleAnimId = Animator.StringToHash("Idle");
        _rb.gravityScale = initialGravityScale;
    }
    public void Die()
    {
        blasrObject.SetActive(true);
        blast.SetTrigger(deathAnimId);
        animator?.SetBool(deathAnimId, true);
        StartCoroutine(Death());
    }
    bool hurt = false;
    IEnumerator Death()
    {
        if(!Audiomanager.Instance._playerHurt.isPlaying) 
            Audiomanager.Instance._playerHurt.Play();

        yield return new WaitForSeconds(0.2f);
        blasrObject.SetActive(false);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Audiomanager.Instance._playerHurt.Stop();
    }
    private void Update()
    {
        IdleAnimation();
        JumpAnimation();
       // _rb.centerOfMass = foot.localPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void IdleAnimation()
    {
        if (IsGrounded())
        {
            animator.SetBool(jumpAnimId, false);
            animator?.SetBool(idleAnimId, true);
        }
    }
    private void JumpAnimation()
    {
        if(transform.position.y < parachuteOpenYAxisValue && !isGrounded)
        {
            animator.SetBool(jumpAnimId, true);
            _rb.gravityScale = parachuteOpenGravityScale;
            animator?.SetBool(idleAnimId, false);
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
