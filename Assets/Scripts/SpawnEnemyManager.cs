using System.Collections;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public BoxCollider2D targetArea;
    [SerializeField] private FloatVariable lowSpawnTreshold;
    [SerializeField] private FloatVariable topSpawnTreshold;
    private float currentTime;
    private float timeToSpawn;

    void Update()
    {
        if (!InputManager.Instance.inputEnabled)
            return;
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSpawn && InputManager.Instance.inputEnabled)
        {
            timeToSpawn = Random.Range(lowSpawnTreshold.value, topSpawnTreshold.value);
            currentTime = 0;
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveToRandomPoint(enemy));
        }
    }

    IEnumerator MoveToRandomPoint(GameObject enemy)
    {
        Vector2 targetPosition = GetRandomPointInCollider(targetArea);
        while (enemy != null && (Vector2)enemy.transform.position != targetPosition)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, 3f * Time.deltaTime);
            yield return null;
        }
    }

    Vector2 GetRandomPointInCollider(BoxCollider2D collider)
    {
        Bounds bounds = collider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}