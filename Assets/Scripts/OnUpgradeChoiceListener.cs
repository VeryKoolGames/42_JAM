using DefaultNamespace;
using UnityEngine;

public class OnUpgradeChoiceListener : MonoBehaviour
{
    public OnUpgradeChoice Event;
    public delegate void OnUpgradeChoiceDelegate(UpgradesTypesEnum type);
    public event OnUpgradeChoiceDelegate OnPowerUpActivation;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(UpgradesTypesEnum type)
    {
        OnPowerUpActivation?.Invoke(type);
    }
}