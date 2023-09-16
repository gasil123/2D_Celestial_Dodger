using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 10;
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
        yield return new WaitForSeconds(5f);
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
