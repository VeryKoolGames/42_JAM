using DefaultNamespace;
using UnityEngine;

public class OnEnemySpawnListener : MonoBehaviour
{
    public OnEnemySpawn Event;
    public delegate void OnPowerUpActivationDelegate(GameObject obj);
    public event OnPowerUpActivationDelegate OnEnemySpawn;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject obj)
    {
        OnEnemySpawn?.Invoke(obj);
    }
}