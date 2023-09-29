using System.Collections;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public const int health = 100;
    private int currentHealth;
    private int currentHealthDecreaseValue = 100;
    private void Start()
    {
        currentHealth = health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<SoldierController>();

        if(enemy != null)
        {
            PlayExplosion();
            DecreaseHealth();
        }
    }
    private void PlayExplosion()
    {
        Audiomanager.Instance._turretHurt.Play();
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        explosion.GetComponent<Animator>().SetTrigger("Death");
    }
    private void DecreaseHealth()
    {
        currentHealth -= currentHealthDecreaseValue;
        CanvasManager.instance.healthSlider.value -= currentHealthDecreaseValue;
        if (currentHealth < 10)
        {
            StartCoroutine(InvokeGameOver());
        }
    }
    IEnumerator InvokeGameOver()
    {
        yield return new WaitForSeconds(1);
        EventManager.gameOver?.Invoke();
    }
}
