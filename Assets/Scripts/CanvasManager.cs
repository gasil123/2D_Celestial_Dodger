using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (startPanlel != null) startPanlel.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex == 1) 
        {
            gamePanel.SetActive(true);
        }
        else
        {
            gamePanel.SetActive(false);
        }
        pauseMenuPanel.SetActive(false);
        if(gameOverPanel!=null)gameOverPanel.SetActive(false);
    }
    public void PauseMenu(bool state)
    {
        pauseMenuPanel.SetActive(state);
        gamePanel.SetActive(!state);
        if(startPanlel!=null)startPanlel.SetActive(!state);
        if (gameOverPanel != null) gameOverPanel.SetActive(!state);
    }
    public void GamePanel(bool state)
    {
        gamePanel.SetActive(state);
        pauseMenuPanel.SetActive(!state);
        if (startPanlel != null) startPanlel.SetActive(!state);
        if (gameOverPanel != null) gameOverPanel.SetActive(!state);
    }
    public void GameOverPanel(bool state)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(state);
        gamePanel.SetActive(!state);
        pauseMenuPanel.SetActive(!state);
        if (startPanlel != null) startPanlel.SetActive(!state);
    }
}
