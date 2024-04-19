using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "OnUpgradeChoice", menuName = "ScriptableObjects/Event/OnUpgradeChoice", order = 1)]
public class OnUpgradeChoice : ScriptableObject
{
    private List<OnUpgradeChoiceListener> _listeners = new List<OnUpgradeChoiceListener>();

    public void RegisterListener(OnUpgradeChoiceListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(OnUpgradeChoiceListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Raise(UpgradesTypesEnum type)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(type);
        }
    }
}
