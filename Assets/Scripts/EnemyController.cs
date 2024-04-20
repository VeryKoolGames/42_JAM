using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;

    [SerializeField] private FloatVariable playerDmg;
    public void UpdateHealth()
    {
        enemyHealth -= playerDmg.value;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
