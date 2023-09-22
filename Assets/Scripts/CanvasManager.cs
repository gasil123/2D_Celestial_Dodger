using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    
    [SerializeField] GameObject startPanlel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject gameOverPanel;
    public Slider healthSlider;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SfxSlider;
    [SerializeField] Toggle muteToggle;

    [SerializeField] int _planeDestroyedScore = 10;
    [SerializeField] int _enemyDestroyedScore = 15;
    [SerializeField] int _bulletCost = 1;

    private const int score = 0;
    private  int currentScore = 0;
    private  int topScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI topScoreText;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        EventManager.planeDestroyed += UpdateScoreTextOnPlaneHit;
        EventManager.enemyDestroyed += UpdateScoreTextOnEnemyHit;
        EventManager.bulletfired += UpdateScoreTextOnBulletFired;
    }
    private void OnDisable()
    {

        EventManager.planeDestroyed -= UpdateScoreTextOnPlaneHit;
        EventManager.enemyDestroyed -= UpdateScoreTextOnEnemyHit;
        EventManager.bulletfired -= UpdateScoreTextOnBulletFired;
    }
    private void Start()
    {
        currentScore = score;
        topScore = PlayerPrefs.GetInt("HighScore");
        if (topScoreText!=null) topScoreText.text = topScore.ToString();
        if (scoreText != null) scoreText.text = currentScore.ToString();
        if (healthSlider!=null) healthSlider.value = 100;
        if (startPanlel != null) startPanlel.SetActive(true);
        if (gameOverPanel!=null)gameOverPanel.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex == 1) 
        {
            gamePanel.SetActive(true);
        }
        else
        {
            gamePanel.SetActive(false);
        }
        pauseMenuPanel.SetActive(false);
        if (musicSlider != null  && SfxSlider!= null && muteToggle!=null)
        {
            musicSlider.onValueChanged.AddListener(Audiomanager.instance.SetMusicVolume);
            SfxSlider.onValueChanged.AddListener(Audiomanager.instance.SetSfxVolume);
            muteToggle.onValueChanged.AddListener(Audiomanager.instance.MuteUnmute);
        }
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

    private void UpdateScoreTextOnPlaneHit()
    {
        Debug.Log("Updating score plane hit");
        scoreText.text = (currentScore += _planeDestroyedScore).ToString();
        UpdateHighScore();
    } 
    private void UpdateScoreTextOnEnemyHit()
    {
        scoreText.text = (currentScore += _enemyDestroyedScore).ToString();
        UpdateHighScore();
    }
    private void UpdateScoreTextOnBulletFired()
    {
        scoreText.text = (currentScore -= _bulletCost).ToString(); 
    }
    private void UpdateHighScore()
    {
        if (currentScore > topScore)
        {
            topScore = currentScore;
            PlayerPrefs.SetInt("HighScore", topScore);
            if (topScoreText != null) topScoreText.text = topScore.ToString();
        }
    }
}
