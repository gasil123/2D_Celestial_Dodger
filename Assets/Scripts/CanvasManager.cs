using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    
    [SerializeField] GameObject startPanlel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pauseMenuPanel;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        startPanlel.SetActive(true);
        gamePanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }
    public void PauseMenu(bool state)
    {
        pauseMenuPanel.SetActive(state);
        gamePanel.SetActive(!state);
        startPanlel.SetActive(!state);
    }
    public void GamePanel(bool state)
    {
        gamePanel.SetActive(state);
        pauseMenuPanel.SetActive(!state);
        startPanlel.SetActive(!state);
    }
}
