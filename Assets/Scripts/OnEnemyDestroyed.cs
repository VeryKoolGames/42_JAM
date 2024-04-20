using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "OnEnemyDestroyed", menuName = "ScriptableObjects/Event/OnEnemyDestroyed", order = 1)]
public class OnEnemyDestroyed : ScriptableObject
{
    private List<OnEnemyDestroyedListener> _listeners = new List<OnEnemyDestroyedListener>();

    public void RegisterListener(OnEnemyDestroyedListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(OnEnemyDestroyedListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Raise(int obj)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(obj);
        }
    }
}
