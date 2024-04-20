using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    private bool isSpectral;
    private ShootingTypesEnum currentShootingType;
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public FloatVariable bulletSpeed;
    public float spreadAngle = 15f;
    public FloatVariable shootRate; // Time in seconds between shots
    public FloatVariable playerDmg; // Time in seconds between shots
    public float currentShootRate; // Time in seconds between shots
    public float currentPlayerDmg; // Time in seconds between shots
    private float nextShootTime = 0f; // When the next shot can happen
    private bool isShooting; // When the next shot can happen

    private void Start()
    {
        GetComponent<OnPowerUpListener>().OnPowerUpActivation += UpdateShootingState;
        currentShootingType = ShootingTypesEnum.BASE;
        currentShootRate = shootRate.value;
        currentPlayerDmg = playerDmg.value;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InputManager.Instance.inputEnabled)
        {
            isShooting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }
        if (isShooting && Time.time >= nextShootTime)
        {
            switch (currentShootingType)
            {
                case ShootingTypesEnum.BASE:
                    Shoot();
                    break;
                case ShootingTypesEnum.CONE:
                    ShootBulletsInCone();
                    break;
            }

            nextShootTime = Time.time + currentShootRate;
        }
    }

    public void UpdateShootingState(ShootingTypesEnum newType)
    {
        if (newType == ShootingTypesEnum.SPECTRAL)
        {
            isSpectral = true;
            StartCoroutine(SpectralCountDown());
        }
        else
        {
            currentShootingType = newType;
            StartCoroutine(PowerUpCountdown());
        }
    }

    private IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(2f);
        currentShootingType = ShootingTypesEnum.BASE;
    }
    
    private IEnumerator SpectralCountDown()
    {
        Color color = bulletPrefab.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        bulletPrefab.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(2f);
        color.a = 1f;
        isSpectral = false;
        bulletPrefab.GetComponent<SpriteRenderer>().color = color;
    }

    void Shoot()
    {
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 shootingDirection = (worldMousePos - transform.position).normalized;
        InstantiateAndShootBullet(shootingDirection);
    }
    
    private void ShootBulletsInCone()
    {
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        Vector2 shootingDirection = (worldMousePos - transform.position).normalized;
        InstantiateAndShootBullet(shootingDirection);
        Vector2 leftDirection = Quaternion.Euler(0, 0, spreadAngle) * shootingDirection;
        InstantiateAndShootBullet(leftDirection);
        Vector2 rightDirection = Quaternion.Euler(0, 0, -spreadAngle) * shootingDirection;
        InstantiateAndShootBullet(rightDirection);
    }

    private void InstantiateAndShootBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        if (isSpectral)
            bullet.GetComponent<BulletController>().isSpectral = true;
        bullet.GetComponent<BulletController>().playerDmg = (int)currentPlayerDmg;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed.value;
    }

    public void UpgradeShootingRate()
    {
        currentShootRate -= 05f;
    }

    public void UpgradePlayerDmg()
    {
        currentPlayerDmg += 10;
    }
}