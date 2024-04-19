using DefaultNamespace;
using UnityEngine;

public class OnPowerUpListener : MonoBehaviour
{
    public OnPowerUpActivation Event;
    public delegate void OnPowerUpActivationDelegate(ShootingTypesEnum foodGained);
    public event OnPowerUpActivationDelegate OnPowerUpActivation;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(ShootingTypesEnum foodGained)
    {
        OnPowerUpActivation?.Invoke(foodGained);
    }
}