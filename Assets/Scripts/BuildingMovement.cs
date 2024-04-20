using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;

    [SerializeField] private OnEnemySpawn onEnemySpawnEvent;

    private void Start()
    {
        onEnemySpawnEvent.Raise(gameObject);
    }

    void Update()
    {
        transform.position += Vector3.left * speed.value * Time.deltaTime;
    }
}
