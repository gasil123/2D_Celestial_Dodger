using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void SetProjectileDirection(Vector3 dir)
    {
        _rb.velocity = (dir * _speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        var enemyScript = collision.gameObject.GetComponent<Enemy>();
        enemyScript?.Die();
        gameObject.SetActive(false);
    }
}
