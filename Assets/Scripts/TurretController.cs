using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class TurretController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f;
    [SerializeField] float maxRotationAngle = 0f;
    [SerializeField] Transform barrelTransform;
    [SerializeField] GameObject fireEffect;

    public bool Automode = false;

    private Rigidbody2D rb;
    private float currentRotation;

    private Animator fireEffectAnimator; 
    float rotationInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireEffectAnimator = fireEffect.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        RotateTurret();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>().x;
    }
    private void RotateTurret()
    {
        float _newRotation = Mathf.Clamp(currentRotation + rotationInput * rotationSpeed * Time.deltaTime, -maxRotationAngle, maxRotationAngle);
        rb.rotation = -_newRotation;
        currentRotation = _newRotation;
    }
    public void Shoot()
    {
        StartCoroutine(StartShooting());
    }
    IEnumerator StartShooting()
    {
        //while (Automode)
        //{
        //    GameObject _bullet = GetBullet();
        //    _bullet.GetComponent<Projectile>().SetProjectileDirection(transform.up);
        //    fireEffectAnimator.GetComponent<Animator>().SetTrigger("shootEffect");
        //    yield return new WaitForSeconds(2);
        //}
        EventManager.bulletfired?.Invoke();
        Audiomanager.instance._turretFire.Play();
        GameObject _bullets = GetBullet();
        _bullets.GetComponent<Projectile>().SetProjectileDirection(transform.up);
        fireEffectAnimator.GetComponent<Animator>().SetTrigger("shootEffect");
        yield return new WaitForSeconds(2);
    }
    public GameObject GetBullet()
    {
        GameObject _bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
        if (_bullet != null)
        {
            _bullet.transform.position = barrelTransform.position;
            _bullet.transform.rotation = barrelTransform.rotation;
            _bullet.SetActive(true);
            return _bullet;
        }
        return null;
    }
}
