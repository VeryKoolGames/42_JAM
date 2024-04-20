using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    public void OnGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        InputManager.Instance.inputEnabled = false;
    }

    public void Restart()
    {
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
        InputManager.Instance.inputEnabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
