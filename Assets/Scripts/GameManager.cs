using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject[] objectSpawners;
    [SerializeField] GameObject turret;
    [SerializeField] Light2D _globalLight;
    [SerializeField] float smoothTime;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        EventManager.gameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.gameOver -= GameOver;
    }
    private void Start()
    {
        PlayGame();
    }
    public void StartGame()
    {
        InititilizeGameState(true);
    }
    public void PauseGame()
    {
        InititilizeGameState(false);
        Time.timeScale = 0;
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        InititilizeGameState(true);
    }
    public void GameOver()
    {
        PauseGame();
        _globalLight.color = Color.Lerp(_globalLight.color, Color.red, 2f);
        Audiomanager.Instance.PlayGameOver();
        CanvasManager.instance.GameOverPanel(true);
    }
    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void RetryLevel1()
    {
        Audiomanager.Instance.StopGameOver();
        Audiomanager.Instance.playBGM();
        SceneManager.LoadScene(1);
    }
    private void InititilizeGameState(bool state)
    {
        foreach (GameObject obj in objectSpawners)
        {
            obj.SetActive(state);
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            turret.GetComponent<TurretController>().enabled = state;
            turret.GetComponent<PlayerInput>().enabled = state;
        }
    }
    public void QuitApplication() 
    {
        Application.Quit();
    }
}