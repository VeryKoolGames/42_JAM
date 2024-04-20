using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 mousePos;
    public bool isSpectral;
    public int playerDmg;

    [SerializeField] private OnPowerUpActivation _onPowerUpActivation;
    [SerializeField] private OnEnemyDestroyed _enemyDestroyed;

    private Camera mainCamera;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            if (!isSpectral)
                Destroy(gameObject);
            bool res = other.gameObject.GetComponent<EnemyController>().UpdateHealth(playerDmg);
            if (!res) return;
            _enemyDestroyed.Raise(other.gameObject.GetComponent<EnemyController>().scoreGained);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PowerUp"))
        {
            _onPowerUpActivation.Raise(other.gameObject.GetComponent<StorePowerUpType>().type);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
