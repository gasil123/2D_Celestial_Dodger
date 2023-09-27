using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class TurretController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f;
    [SerializeField] float maxRotationAngle = 0f;
    [SerializeField] Transform barrelTransform;
    [SerializeField] GameObject fireEffect;
    [SerializeField] PlayerInput _playerInput;
    private InputAction shootAction;

    private Action<InputAction.CallbackContext> shootActionCallback;

    private Rigidbody2D rb;
    private float currentRotation;

    private Animator fireEffectAnimator; 
    float rotationInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireEffectAnimator = fireEffect.GetComponent<Animator>();
       
    }
    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        shootAction = _playerInput.actions["Fire"];
        shootActionCallback = _ => StartCoroutine(StartShooting());
        shootAction.performed += shootActionCallback;
    }
    private void OnDisable()
    {
        shootAction.performed -= shootActionCallback;
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
    IEnumerator StartShooting()
    {
        EventManager.bulletfired?.Invoke();
        Audiomanager.Instance._turretFire.Play();
        GameObject _bullets = GetBullet();
        _bullets.GetComponent<Projectile>().SetProjectileDirection(transform.up);
        fireEffectAnimator.GetComponent<Animator>().SetTrigger("shootEffect");
        yield return null;
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
