using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UITitle : GameBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        _GM.ChangedGameState(GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
