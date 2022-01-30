using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    void Start()
    {
        PlayerController.PlayerDiedEvent += OnGameOver;
    }

    void OnGameOver(){
        gameOverUI.SetActive(true);
    }

    public void RetryButton(){
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
