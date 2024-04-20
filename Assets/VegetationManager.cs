using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    public List<GameObject> vegetationList;

    public void GenerateVegetation(Transform pos)
    {
        HashSet<int> indices = new HashSet<int>();
        while (indices.Count < 3)
        {
            indices.Add(Random.Range(0, vegetationList.Count));
        }

        foreach (int index in indices)
        {
            Vector3 randomPosition = pos.position + new Vector3(Random.Range(-.02f, .02f), 0, Random.Range(-.02f, .02f));
            Instantiate(vegetationList[index], randomPosition, Quaternion.identity);
        }
    }
}
