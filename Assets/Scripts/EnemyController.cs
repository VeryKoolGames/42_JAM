using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    // Start is called before the first frame update
    public void UpdateHealth()
    {
        enemyHealth -= 10;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
