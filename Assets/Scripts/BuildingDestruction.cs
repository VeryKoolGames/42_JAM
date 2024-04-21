using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    public float shrinkDuration;
    public GameObjectsSO vegetationList;
    public GameObjectsSO vegetationPatchList;
    public GameObject particles;
    public Transform particleSpawnPoint;
    private float botTreshold = .2f;
    private float topTreshold = .2f;

    public IEnumerator OnBuildingDestroyed()
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0;

        Instantiate(particles, particleSpawnPoint.position, Quaternion.identity);

        while (elapsedTime < shrinkDuration)
        {
            float progress = elapsedTime / shrinkDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        HashSet<int> indices = new HashSet<int>();
        while (indices.Count < 3)
        {
            indices.Add(Random.Range(0, vegetationList.Objects.Count));
        }

        GameObject patch = vegetationPatchList.Objects[Random.Range(0, vegetationPatchList.Objects.Count)];

        foreach (int index in indices)
        {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-botTreshold, topTreshold), Random.Range(-botTreshold, topTreshold), 0);
            Instantiate(vegetationList.Objects[index], randomPosition, Quaternion.identity);
        }
        Vector3 randPosition = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
        Instantiate(patch, randPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
