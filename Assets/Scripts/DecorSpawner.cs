using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject smallSign;
    [SerializeField] private GameObject BigSign;
    
    [SerializeField] private Transform PossmallSign1;
    [SerializeField] private Transform PossmallSign2;
    [SerializeField] private Transform PosBigSign1;
    [SerializeField] private Transform PosBigSign2;

    [SerializeField] private float delayBetweenSpawn;
    [SerializeField] private float currentTime;
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= delayBetweenSpawn && InputManager.Instance.inputEnabled)
        {
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    spawnBigSignes();
                    break;
                case 1:
                    spawnSmallSignes();
                    break;
            }

            currentTime = 0f;
        }
    }

    private void spawnSmallSignes()
    {
        Instantiate(smallSign, PossmallSign1.position, Quaternion.identity);
        Instantiate(smallSign, PossmallSign2.position, Quaternion.identity);
    }
    
    private void spawnBigSignes()
    {
        Instantiate(BigSign, PosBigSign1.position, Quaternion.identity);
        Instantiate(BigSign, PosBigSign2.position, Quaternion.identity);
    }
}
