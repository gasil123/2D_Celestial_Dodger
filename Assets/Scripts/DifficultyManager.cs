using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] float initialTimeScale = 1.0f; 
    [SerializeField] float maxTimeScale = 2.0f; 
    [SerializeField] float increaseInterval = 10.0f; 
    [SerializeField] float increaseAmount = 0.1f;
    private float timeSinceLastIncrease = 0.0f;

    void Update()
    {
        timeSinceLastIncrease += Time.deltaTime;

        if (timeSinceLastIncrease >= increaseInterval)
        {
            if (Time.timeScale < maxTimeScale)
            {
                Time.timeScale += increaseAmount;
                Time.timeScale = Mathf.Clamp(Time.timeScale, initialTimeScale, maxTimeScale);
            }

            timeSinceLastIncrease = 0.0f;
        }
    }
}
