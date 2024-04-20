using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public FloatVariable playerMaxHealth;
    private float playerHealth;
    [SerializeField] private GameOverManager _gameOverManager;

    private void Start()
    {
        playerHealth = playerMaxHealth.value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            playerHealth -= 10;
            Destroy(other.gameObject);
            if (playerHealth <= 0)
            {
                OnGameOver();
            }
        }
    }

    private void OnGameOver()
    {
        _gameOverManager.OnGameOver();
    }

    public void UpgradeHealth()
    {
        playerHealth += 20;
    }
}
