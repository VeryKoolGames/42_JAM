using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    public float shrinkDuration;
    public GameObjectsSO vegetationList;
    [SerializeField] private float botTreshold = .5f;
    [SerializeField] private float topTreshold = .5f;

    public IEnumerator OnBuildingDestroyed()
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0;

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

        foreach (int index in indices)
        {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-botTreshold, topTreshold), Random.Range(-botTreshold, topTreshold), 0);
            Instantiate(vegetationList.Objects[index], randomPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
