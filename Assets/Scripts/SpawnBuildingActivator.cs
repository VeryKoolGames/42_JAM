using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingActivator : MonoBehaviour
{
    [SerializeField] private SpawnBuildingsManager _spawnBuildingsManager;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _spawnBuildingsManager.SpawnBuilding();
        }
    }
}
