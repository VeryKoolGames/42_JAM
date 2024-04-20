using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private AudioSource powerUpSound;
    [SerializeField] private Transform shootPos;
    private bool isSpectral;
    private ShootingTypesEnum currentShootingType;
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public FloatVariable bulletSpeed;
    public float spreadAngle = 15f;
    public FloatVariable shootRate; // Time in seconds between shots
    public FloatVariable playerDmg; // Time in seconds between shots
    public FloatVariable drillTimeLeft; // Temps restant de la drill (je parle pas anglais en fait pauvre fou)
    public FloatVariable tripleShotTimeLeft; // Temps restant du tripleShot
    public FloatVariable powerUpDuration; // Durée en secondes des powerUps
    public float currentShootRate; // Time in seconds between shots
    public float currentPlayerDmg; // Time in seconds between shots
    private float nextShootTime = 0f; // When the next shot can happen
    private bool isShooting; // When the next shot can happen
    public float powerUpTime; // Temps écoulé depuis que le powerUp est actif
    public CameraShake cameraShakeScript;
    
    


    private void Start()
    {
        GetComponent<OnPowerUpListener>().OnPowerUpActivation += UpdateShootingState;
        currentShootingType = ShootingTypesEnum.BASE;
        currentShootRate = shootRate.value;
        currentPlayerDmg = playerDmg.value;
        drillTimeLeft.value = powerUpDuration.value;
        tripleShotTimeLeft.value = powerUpDuration.value;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InputManager.Instance.inputEnabled)
        {
            gunSound.Play();
            isShooting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            gunSound.Stop();
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
        powerUpSound.Play();
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
        while (powerUpTime < powerUpDuration.value)
        {
            tripleShotTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, powerUpTime / powerUpDuration.value);
            powerUpTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(powerUpDuration.value);
        currentShootingType = ShootingTypesEnum.BASE;
        tripleShotTimeLeft.value = 0;
        powerUpTime = 0;
    }
    
    private IEnumerator SpectralCountDown()
    {
        while (powerUpTime < powerUpDuration.value)
        {
            drillTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, powerUpTime / powerUpDuration.value);
            powerUpTime += Time.deltaTime;
            yield return null;
        }

        Color color = bulletPrefab.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        bulletPrefab.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(powerUpDuration.value);
        color.a = 1f;
        isSpectral = false;
        bulletPrefab.GetComponent<SpriteRenderer>().color = color;
        drillTimeLeft.value = 0;
        powerUpTime = 0;
    }

    void Shoot()
    {
        cameraShakeScript.ShakeCamera();
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 shootingDirection = (worldMousePos - transform.position).normalized;
        InstantiateAndShootBullet(shootingDirection);
    }
    
    private void ShootBulletsInCone()
    {
        cameraShakeScript.ShakeCamera();
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
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        if (isSpectral)
            bullet.GetComponent<BulletController>().isSpectral = true;
        bullet.GetComponent<BulletController>().playerDmg = (int)currentPlayerDmg;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed.value;
    }

    public void UpgradeShootingRate()
    {
        currentShootRate -= .05f;
    }

    public void UpgradePlayerDmg()
    {
        currentPlayerDmg += 10;
    }
}
