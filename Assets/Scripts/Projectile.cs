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
        var enemyScript = collision.gameObject.GetComponent<EnemyShip>();
        var soldierScript = collision.gameObject.GetComponent<SoldierController>();
        if (enemyScript != null)
        {
            EventManager.planeDestroyed?.Invoke();
            enemyScript?.Die();
        } 
        else if (soldierScript != null)
        {
            EventManager.enemyDestroyed?.Invoke();
            soldierScript?.Die();
        }
        gameObject.SetActive(false);
    }
}
