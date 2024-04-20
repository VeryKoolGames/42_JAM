using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;
    public bool isABuilding;
    public int scoreGained;
    public Material originalMaterial;
    public Material damageMaterial;
    [SerializeField] private OnEnemySpawn onEnemySpawnEvent;

    private void Start()
    {
        onEnemySpawnEvent.Raise(gameObject);
    }

    public bool UpdateHealth(int playerDmg)
    {
        enemyHealth -= playerDmg;
        StartCoroutine(FlashDamageEffect());
        if (enemyHealth <= 0)
        {
            if (isABuilding)
            {
                StartCoroutine(GetComponent<BuildingDestruction>().OnBuildingDestroyed());
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
}
