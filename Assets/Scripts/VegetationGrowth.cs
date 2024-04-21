using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGrowth : MonoBehaviour
{
    [SerializeField] private float growDuration = 0.2f;
    void Start()
    {
        StartCoroutine(GrowVegetation());
    }

    
    public IEnumerator GrowVegetation()
    {
        Vector3 zeroScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;
        float elapsedTime = 0;

        while (elapsedTime < growDuration)
        {
            float progress = elapsedTime / growDuration;
            transform.localScale = Vector3.Lerp(zeroScale, targetScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
