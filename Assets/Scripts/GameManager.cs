using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        InititilizeGameState(false);
    }
    public void StartGame()
    {
        InititilizeGameState(true);
    }
    public void StopGame()
    {
        InititilizeGameState(false);
    }
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }
    public static void PlayGame()
    {
        Time.timeScale = 1;
    }

    private void InititilizeGameState(bool state)
    {
        foreach (GameObject obj in objectSpawners)
        {
            obj.SetActive(state);
        }
        turret.GetComponent<TurretController>().enabled = state;
        turret.GetComponent<PlayerInput>().enabled = state;
        CanvasManager.instance.GamePanel(state);
    }
}