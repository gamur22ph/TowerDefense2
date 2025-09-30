using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    private enum GameState {
        InGame,
        Paused,
        Result
    }

    public float GameTime { get; private set; }
    private GameState state;
    private float currentGameSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerManager.instance.OnLifeChanged += OnPlayerLifeChanged;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
        PlayerManager.instance.OnLifeChanged -= OnPlayerLifeChanged;
    }

    private void OnPlayerLifeChanged(float currentLives)
    {
        if (currentLives <= 0)
        {
            HandleFailed();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.InGame:
                GameTime += Time.deltaTime;
                break;
        }
    }

    public void ChangeSpeedToNormal()
    {
        currentGameSpeed = 1;
        Time.timeScale = currentGameSpeed;
    }

    public void ChangeSpeedToFast()
    {
        currentGameSpeed = 2;
        Time.timeScale = currentGameSpeed;
    }

    public void HandleFailed()
    {
        state = GameState.Result;
        Debug.Log("Failed");
        Time.timeScale = 0;
    }

    public void HandleSuccess()
    {
        state = GameState.Result;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        state = GameState.Paused;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        state = GameState.InGame;
        Time.timeScale = currentGameSpeed;
    }

    public bool IsGamePaused()
    {
        return state == GameState.Paused;
    }
}
