using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "OnEnemySpawn", menuName = "ScriptableObjects/Event/OnEnemySpawn", order = 1)]
public class OnEnemySpawn : ScriptableObject
{
    private List<OnEnemySpawnListener> _listeners = new List<OnEnemySpawnListener>();

    public void RegisterListener(OnEnemySpawnListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(OnEnemySpawnListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Raise(GameObject obj)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(obj);
        }
    }
}
