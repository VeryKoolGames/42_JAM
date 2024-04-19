using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "OnPowerUpActivation", menuName = "ScriptableObjects/Event/OnPowerUpActivation", order = 1)]
public class OnPowerUpActivation : ScriptableObject
{
    private List<OnPowerUpListener> _listeners = new List<OnPowerUpListener>();

    public void RegisterListener(OnPowerUpListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(OnPowerUpListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Raise(ShootingTypesEnum type)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(type);
        }
    }
}
