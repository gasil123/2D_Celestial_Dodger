using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject[] objectSpawners;
    [SerializeField] GameObject turret;
    

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
        CanvasManager.instance.GameOverPanel(true);
    }
    private void InititilizeGameState(bool state)
    {
        foreach (GameObject obj in objectSpawners)
        {
            obj.SetActive(state);
        }
        turret.GetComponent<TurretController>().enabled = state;
        turret.GetComponent<PlayerInput>().enabled = state;
    }
}