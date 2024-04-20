using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public FloatVariable playerMaxHealth;
    private float playerHealth;
    [SerializeField] private GameOverManager _gameOverManager;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        playerHealth = playerMaxHealth.value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            playerHealth -= 10;
            healthBar.value -= 10;
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
        healthBar.maxValue += 20;
        healthBar.value = healthBar.maxValue;
        playerHealth += 20;
    }
}
