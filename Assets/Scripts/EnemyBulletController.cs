using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 3f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet Encountered");
            if (!other.gameObject.GetComponent<BulletController>().isSpectral)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
