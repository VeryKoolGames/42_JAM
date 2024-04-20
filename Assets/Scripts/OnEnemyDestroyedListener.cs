using DefaultNamespace;
using UnityEngine;

public class OnEnemyDestroyedListener : MonoBehaviour
{
    public OnEnemyDestroyed Event;
    public delegate void OnPowerUpActivationDelegate(int scoreToBeAdded);
    public event OnPowerUpActivationDelegate OnEnemyDestroyed;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(int scoreToBeAdded)
    {
        OnEnemyDestroyed?.Invoke(scoreToBeAdded);
    }
}