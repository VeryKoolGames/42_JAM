using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariable TimeBeforeStation;
    [SerializeField] private FloatVariable globalSpeed;
    [SerializeField] private GameObject stationCanvas;
    [SerializeField] private GameObject stationObject;
    private List<GameObject> ObjectToBeDestroyed = new List<GameObject>();
    private float originalTime;
    private float originalSpeed;
    private bool isInStation;
    // private bool accelerationSpeed = 0.9f;
    public bool shouldTrainStop;
    
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        originalTime = TimeBeforeStation.value;
        originalSpeed = globalSpeed.value;
        GetComponent<OnEnemySpawnListener>().OnEnemySpawn += addObject;
    }

    public void addObject(GameObject newObj)
    {
        ObjectToBeDestroyed.Add(newObj);
    }

    void Update()
    {
        originalTime -= Time.deltaTime;
        if (originalTime <= 0f && !isInStation && shouldTrainStop)
        {
            isInStation = true;
            InputManager.Instance.inputEnabled = false;
            DestroyEnemies();
            StartCoroutine(startStationSequence());
        }
    }

    public void StartGameAgain()
    {
        stationCanvas.SetActive(false);
        StartCoroutine(startGameAgain());
    }

    private IEnumerator startStationSequence()
    {
        stationObject.GetComponent<StationMovement>().shouldMove = true;
        float duration = 9f;
        float startValue = globalSpeed.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            globalSpeed.value = Mathf.Lerp(startValue, 0, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        globalSpeed.value = 0;
        yield return new WaitForSeconds(2f);
        stationCanvas.SetActive(true);
    }

    private void DestroyEnemies()
    {
        foreach (var obj in ObjectToBeDestroyed)
        {
            Destroy(obj);
        }
        ObjectToBeDestroyed.Clear();
    }
    
    private IEnumerator startGameAgain()
    {
        float duration = 9f; // Total time over which the value should increase
        float startValue = 0f; // Starting value of the float, assuming it starts from 0
        float elapsedTime = 0f; // Time elapsed since the start of the increase

        while (elapsedTime < duration)
        {
            globalSpeed.value = Mathf.Lerp(startValue, originalSpeed, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        yield return new WaitForSeconds(2f);
        InputManager.Instance.inputEnabled = true;
        originalTime = TimeBeforeStation.value;
        isInStation = false;
    }
}
