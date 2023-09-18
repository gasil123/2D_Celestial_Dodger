using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    
    [SerializeField] GameObject startPanlel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject gameOverPanel;
    public Slider healthSlider;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        healthSlider.value = 100;
        startPanlel?.SetActive(true);
        gamePanel?.SetActive(false);
        pauseMenuPanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
    }
    public void PauseMenu(bool state)
    {
        pauseMenuPanel?.SetActive(state);
        gamePanel?.SetActive(!state);
        startPanlel?.SetActive(!state);
        gameOverPanel?.SetActive(!state);
    }
    public void GamePanel(bool state)
    {
        gamePanel?.SetActive(state);
        pauseMenuPanel?.SetActive(!state);
        startPanlel?.SetActive(!state);
        gameOverPanel?.SetActive(!state);
    }
    public void GameOverPanel(bool state)
    {
        gameOverPanel?.SetActive(state);
        gamePanel?.SetActive(!state);
        pauseMenuPanel?.SetActive(!state);
        startPanlel?.SetActive(!state);
    }
}
