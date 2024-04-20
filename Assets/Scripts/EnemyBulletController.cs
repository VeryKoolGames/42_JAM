using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Vector2 targetPosition;
    [SerializeField] private OnEnemySpawn onEnemySpawnEvent;

    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        onEnemySpawnEvent.Raise(gameObject);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
    }

}
