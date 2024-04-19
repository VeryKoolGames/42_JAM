using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 mousePos;
    public bool isSpectral;

    [SerializeField] private OnPowerUpActivation _onPowerUpActivation;

    private Camera mainCamera;

    private Rigidbody2D rb;

    public float speed;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isSpectral)
                Destroy(gameObject);
            other.gameObject.GetComponent<EnemyController>().UpdateHealth();
        }
        else if (other.CompareTag("PowerUp"))
        {
            _onPowerUpActivation.Raise(other.gameObject.GetComponent<StorePowerUpType>().type);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
