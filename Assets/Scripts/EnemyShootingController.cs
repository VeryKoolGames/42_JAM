using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class EnemyShootingController : MonoBehaviour
    {
        [SerializeField] private GameObject enemeyBulletPrefab;
        private bool canShoot;
        private float delayBeforeShooting;
        [SerializeField] private float delayBetweenShot;

        private void Update()
        {
            delayBeforeShooting += Time.deltaTime;
            if (delayBeforeShooting > 1.5f)
            {
                canShoot = true;
            }

            if (canShoot && InputManager.Instance.inputEnabled)
            {
                canShoot = false;
                delayBeforeShooting = 0f;
                StartCoroutine(StartShooting());
            }
        }

        private IEnumerator StartShooting()
        {
            Instantiate(enemeyBulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenShot);
            canShoot = true;
        }
    }
}