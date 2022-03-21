using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class Ui_Manager : GameBehaviour<Ui_Manager>
{
    public TMP_Text scoreText;
    public GameObject mainPanel;
    public GameObject pausePanel;
    void Start()
    {
        ScoreChanger(0);
    }
    void ScoreChanger(int _score)
    {
        scoreText.text = "Score:" + _GM.score;
    }
    private void OnEnable()
    {
        GameEvents.OnGameStateChange += OnGameStateChange;
        GameEvents.OnScoreChange += ScoreChanger;
    }
    private void OnDisable()
    {
        GameEvents.OnScoreChange -= ScoreChanger;
    }
    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
    void OnGameStateChange(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Playing:
                mainPanel.SetActive(true);
                pausePanel.SetActive(false);
                break;
            case GameState.Paused:
                mainPanel.SetActive(false);
                pausePanel.SetActive(true);
                break;
            case GameState.GameOver:
                break;

        }
    }

}
