using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariable TimeBeforeStation;
    [SerializeField] private GameObject stationCanvas;
    private List<GameObject> ObjectToBeDestroyed = new List<GameObject>();
    private float originalTime;
    private bool isInStation;
    
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        originalTime = TimeBeforeStation.value;
        GetComponent<OnEnemySpawnListener>().OnEnemySpawn += addObject;
    }

    public void addObject(GameObject newObj)
    {
        ObjectToBeDestroyed.Add(newObj);
    }

    void Update()
    {
        originalTime -= Time.deltaTime;
        if (originalTime <= 0f && !isInStation)
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
        yield return new WaitForSeconds(2f);
        InputManager.Instance.inputEnabled = true;
        originalTime = TimeBeforeStation.value;
        isInStation = false;
    }
}
