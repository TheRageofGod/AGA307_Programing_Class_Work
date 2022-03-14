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
    void Start()
    {
       // gameState = GameState.Start;
        difficulty = Difficulty.Easy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameState = GameState.Playing;
            GameEvents.ReportGameStateChange(gameState);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameState = GameState.Paused;
            GameEvents.ReportGameStateChange(gameState);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameState = GameState.GameOver;
            GameEvents.ReportGameStateChange(gameState);
        }
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
}
