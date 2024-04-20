using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatVariable TimeBeforeStation;
    [SerializeField] private FloatVariable globalSpeed;
    [SerializeField] private GameObject stationCanvas;
    [SerializeField] private GameObject stationObject;
    [SerializeField] private AudioSource departingSound;
    [SerializeField] private AudioSource runningSound;
    [SerializeField] private SpawnBuildingsManager topSpawner;
    [SerializeField] private SpawnBuildingsManager botSpawner;
    private List<GameObject> ObjectToBeDestroyed = new List<GameObject>();
    private float originalTime;
    private float originalSpeed;
    private bool isInStation;

    [SerializeField] private SpawnEnemyManager leftSpawner;
    [SerializeField] private SpawnEnemyManager rightSpawner;
    // private bool accelerationSpeed = 0.9f;
    public bool shouldTrainStop;
    public static GameManager Instance;

    public Animator animator;
    private void Awake()
    {
        globalSpeed.value = 0.85f;
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
        animator.SetTrigger("close");
        departingSound.Play();
        topSpawner.shouldSpawn = true;
        botSpawner.shouldSpawn = true;
        topSpawner.SpawnBuilding();
        botSpawner.SpawnBuilding();
        InputManager.Instance.inputEnabled = true;
        StartCoroutine(startGameAgain());
    }

    private void MakeGameHarder()
    {
        if (leftSpawner.topSpawnTreshold < 4)
        {
            return;
        }
        leftSpawner.topSpawnTreshold -= 1;
        rightSpawner.topSpawnTreshold -= 1;
    }

    private IEnumerator startStationSequence()
    {
        MakeGameHarder();
        runningSound.volume = 0f;
        topSpawner.shouldSpawn = false;
        botSpawner.shouldSpawn = false;
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
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("open");
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

        runningSound.volume = .1f;
        stationCanvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        originalTime = TimeBeforeStation.value;
        isInStation = false;
    }
}
