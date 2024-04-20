using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;
    public bool isABuilding;
    public int scoreGained;
    public Material originalMaterial;
    public Material damageMaterial;
    [SerializeField] private OnEnemySpawn onEnemySpawnEvent;
    [SerializeField] private AudioSource hitSound;

    private void Start()
    {
        if (!isABuilding)
        {
            onEnemySpawnEvent.Raise(gameObject);
        }
        
    }

    public bool UpdateHealth(int playerDmg)
    {
        enemyHealth -= playerDmg;
        StartCoroutine(FlashDamageEffect());
        playHitSound();
        if (enemyHealth <= 0)
        {
            if (isABuilding)
            {
                StartCoroutine(GetComponent<BuildingDestruction>().OnBuildingDestroyed());
            }
            else
            {
                Destroy(gameObject);
            }
            return true;
        }
        return false;
    }
    
    private IEnumerator FlashDamageEffect()
    {
        GetComponent<Renderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material = originalMaterial;
    }

    private void playHitSound()
    {
        hitSound.pitch = Random.Range(0.8f, 1.2f);
        hitSound.Play();
    }
}
