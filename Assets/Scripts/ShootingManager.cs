using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private AudioSource powerUpSound;
    [SerializeField] private Transform shootPos;
    [SerializeField] private PowerUpUIHandler powerUpUIHandler;
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
    private bool isSpectralOn; // When the next shot can happen
    private bool isConeOn; // When the next shot can happen
    private Color bulletColor; // When the next shot can happen
    public float SpectralpowerUpTime; // Temps écoulé depuis que le powerUp est actif
    public float ConepowerUpTime; // Temps écoulé depuis que le powerUp est actif
    public CameraShake cameraShakeScript;

    public Animator animator;
    
    


    private void Start()
    {
        GetComponent<OnPowerUpListener>().OnPowerUpActivation += UpdateShootingState;
        currentShootingType = ShootingTypesEnum.BASE;
        currentShootRate = shootRate.value;
        currentPlayerDmg = playerDmg.value;
        drillTimeLeft.value = powerUpDuration.value;
        tripleShotTimeLeft.value = powerUpDuration.value;
        bulletColor = bulletPrefab.GetComponent<SpriteRenderer>().color;
    }

    private void HandleSpectral()
    {
        if (isSpectralOn)
        {
            SpectralpowerUpTime += Time.deltaTime;
            if (SpectralpowerUpTime > powerUpDuration.value)
            {
                isSpectralOn = false;
                bulletColor.a = 1f;
                isSpectral = false;
                bulletPrefab.GetComponent<SpriteRenderer>().color = bulletColor;
                powerUpUIHandler.MovePowerBackUp(ShootingTypesEnum.SPECTRAL);
                drillTimeLeft.value = 0;
            }
            drillTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, SpectralpowerUpTime / powerUpDuration.value);
        }
    }
    
    private void HandleCone()
    {
        if (isConeOn)
        {
            ConepowerUpTime += Time.deltaTime;
            if (ConepowerUpTime > powerUpDuration.value)
            {
                isConeOn = false;
                powerUpUIHandler.MovePowerBackUp(ShootingTypesEnum.CONE);
                currentShootingType = ShootingTypesEnum.BASE;
                tripleShotTimeLeft.value = 0;
                ConepowerUpTime = 0;
            }
            tripleShotTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, ConepowerUpTime / powerUpDuration.value);        }
    }

    void Update()
    {
        HandleSpectral();
        HandleCone();
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
            powerUpUIHandler.MovePowerUpDown(ShootingTypesEnum.SPECTRAL);
            SpectralpowerUpTime = 0;
            Color color = bulletPrefab.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            bulletPrefab.GetComponent<SpriteRenderer>().color = color;
            isSpectral = true;
            isSpectralOn = true;
        }
        else
        {
            powerUpUIHandler.MovePowerUpDown(ShootingTypesEnum.CONE);
            ConepowerUpTime = 0;
            currentShootingType = newType;
            isConeOn = true;
        }
    }

    // private IEnumerator PowerUpCountdown()
    // {
    //     powerUpUIHandler.MovePowerUpDown(ShootingTypesEnum.CONE);
    //     while (ConepowerUpTime < powerUpDuration.value)
    //     {
    //         tripleShotTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, ConepowerUpTime / powerUpDuration.value);
    //         ConepowerUpTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     yield return new WaitForSeconds(powerUpDuration.value);
    //     currentShootingType = ShootingTypesEnum.BASE;
    //     tripleShotTimeLeft.value = 0;
    //     ConepowerUpTime = 0;
    // }
    
    // private IEnumerator SpectralCountDown()
    // {
    //     powerUpUIHandler.MovePowerUpDown(ShootingTypesEnum.SPECTRAL);
    //     while (SpectralpowerUpTime < powerUpDuration.value)
    //     {
    //         drillTimeLeft.value = Mathf.Lerp(powerUpDuration.value, 0, SpectralpowerUpTime / powerUpDuration.value);
    //         SpectralpowerUpTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     powerUpUIHandler.MovePowerBackUp(ShootingTypesEnum.SPECTRAL);
    //     SpectralpowerUpTime = 0;
    // }

    void Shoot()
    {
        cameraShakeScript.ShakeCamera();
        animator.SetTrigger("shoot");
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 shootingDirection = (worldMousePos - transform.position).normalized;
        InstantiateAndShootBullet(shootingDirection);
    }
    
    private void ShootBulletsInCone()
    {
        cameraShakeScript.ShakeCamera();
        animator.SetTrigger("shoot");
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
        currentShootRate -= .02f;
    }

    public void UpgradePlayerDmg()
    {
        currentPlayerDmg += 5;
    }
}
