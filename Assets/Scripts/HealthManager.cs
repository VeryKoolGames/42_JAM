using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HealthManager : MonoBehaviour
{
    public FloatVariable playerMaxHealth;
    private float playerHealth;
    [SerializeField] private GameOverManager _gameOverManager;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject turretHead;
    [SerializeField] private GameObject turretFoot;
    [SerializeField] private AudioSource[] hitSounds;
    public Material originalMaterial;
    public Material damageMaterial;

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
            PlayRandomHitSound();
            StartCoroutine(FlashDamageEffect());
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
    
    private IEnumerator FlashDamageEffect()
    {
        turretHead.GetComponent<Renderer>().material = damageMaterial;
        turretFoot.GetComponent<Renderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        turretHead.GetComponent<Renderer>().material = originalMaterial;
        turretFoot.GetComponent<Renderer>().material = originalMaterial;
    }

    private void PlayRandomHitSound()
    {
        int idx = Random.Range(0, hitSounds.Length);
        hitSounds[idx].Play();
    }
}
