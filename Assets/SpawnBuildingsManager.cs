using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> BuildingsPrefabs = new List<GameObject>();
    [SerializeField] private FloatVariable speed;
    [SerializeField] private float delay;
    public bool shouldSpawn = true;

    private static int orderInLayer;
    // Start is called before the first frame update

    private void Start()
    {
        spawnBuilding();
    }

    public void SpawnBuilding()
    {
        if (!shouldSpawn)
            return;
        spawnBuilding();
    }

    private void spawnBuilding()
    {
        StartCoroutine(delaySpawn());
    }

    IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(delay);
        int random = Random.Range(0, BuildingsPrefabs.Count);
        float yOffset = Random.Range(-0.02f, 0.02f);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        GameObject obj = Instantiate(BuildingsPrefabs[random], spawnPosition, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer++;
        if (orderInLayer > 10)
            orderInLayer = 0;
    }
    
}
