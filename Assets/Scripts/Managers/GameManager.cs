using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Start,
    Playing,
    Paused,
    GameOver
}

public enum Difficulty
{
    Easy, Medium, Hard
}

public class GameManager : GameBehaviour<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    int scoreMultiplier = 1;
    public int score = 0;
    bool isPaused = false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void ChangedGameState(GameState _gameState)
    {
        gameState = _gameState;
        GameEvents.ReportGameStateChange(gameState);

    }
    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty; // this is a workaround for the drop down because it can see enums only numbers
    }
    void SetUp()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                break;
            case Difficulty.Medium:
                scoreMultiplier = 2;
                break;
            case Difficulty.Hard:
                scoreMultiplier = 3;
                break;
            default:
                scoreMultiplier = 1;
                break;

        }
    }
    public void AddScore(int _value)
    {
       score += _value * scoreMultiplier;
        GameEvents.ReportScoreChange(score);

    }
    void OnEnemyHit(Enemy _enemy)
    {
        AddScore(10);
    }
    void OnEnemyDied(Enemy _enemy)
    {
        AddScore(100);
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyHit += OnEnemyHit;
        GameEvents.OnEnemyDied += OnEnemyDied;
    }
    private void OnDisable()
    {
        GameEvents.OnEnemyHit -= OnEnemyHit;
        GameEvents.OnEnemyDied += OnEnemyDied;
    }
    public void TogglePause()
    {
        //if (gameState != GameState.Paused || gameState != GameState.Playing)
            //return;
        isPaused = !isPaused;
        if (isPaused)
        {
            ChangedGameState(GameState.Paused);
            Time.timeScale = 0;
           
        }
        else
        {
            ChangedGameState(GameState.Playing);
            Time.timeScale = 1;
        }
    }
}
