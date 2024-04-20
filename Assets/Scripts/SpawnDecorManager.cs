using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnDecorManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> PowerUpPrefabs = new List<GameObject>();
    [SerializeField] private FloatVariable speed;
    [SerializeField] private FloatVariable lowSpawnTreshold;
    [SerializeField] private FloatVariable topSpawnTreshold;
    private float currentTime;
    private float timeToSpawn;
    // Start is called before the first frame update

    private void Start()
    {
        timeToSpawn = Random.Range(lowSpawnTreshold.value, topSpawnTreshold.value);
    }

    void Update()
    {
        if (!InputManager.Instance.inputEnabled)
            return;
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSpawn)
        {
            int random = Random.Range(0, 10);
            if (random > 5)
            {
                spawnPowerUp();
            }
            timeToSpawn = Random.Range(lowSpawnTreshold.value, topSpawnTreshold.value);
            currentTime = 0;
        }
    }
    
    private void spawnPowerUp()
    {
        int random = Random.Range(0, PowerUpPrefabs.Count);
        float yOffset = Random.Range(-0.02f, 0.02f);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        Instantiate(PowerUpPrefabs[random], spawnPosition, Quaternion.identity);
    }
}
