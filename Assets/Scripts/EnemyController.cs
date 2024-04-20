using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;
    public int scoreGained;
    [SerializeField] private OnEnemySpawn onEnemySpawnEvent;

    private void Start()
    {
        onEnemySpawnEvent.Raise(gameObject);
    }

    public bool UpdateHealth(int playerDmg)
    {
        enemyHealth -= playerDmg;
        if (enemyHealth <= 0)
        {
            return true;
        }
        return false;
    }
}
