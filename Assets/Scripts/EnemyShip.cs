using System.Collections;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] float _speed = 10;
    [SerializeField] float deactivateTime;
    [SerializeField] GameObject _yellowBlast;
    private Animator _animator;
    public enum states
    {
        startMoving,
    }
    private Rigidbody2D _rb;
    private void OnEnable()
    {
        _animator = _yellowBlast.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        SetDirection(transform.right);
        StartCoroutine(Deactivate());
        _yellowBlast.SetActive(false);
    }
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(deactivateTime);
        gameObject.SetActive(false);
    }
    public void SetDirection(Vector3 dir)
    {
        _rb.velocity = (dir * _speed);
    }
    public void Die()
    {
        _yellowBlast.SetActive(true);
        _animator.SetTrigger("Death");
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.16f);
        gameObject.SetActive(false);
    }
}
