using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private FloatVariable playerHealth;
    [SerializeField] private FloatVariable shootRate;
    [SerializeField] private FloatVariable playerDmg;
    void Start()
    {
        GetComponent<OnUpgradeChoiceListener>().OnPowerUpActivation += EventTrigger;
    }

    public void EventTrigger(UpgradesTypesEnum type)
    {
        switch (type)
        {
            case UpgradesTypesEnum.UPGRADEHEALTH:
                GetComponent<HealthManager>().UpgradeHealth();
                break;
            case UpgradesTypesEnum.UPGRADEBULLETSPEED:
                GetComponent<ShootingManager>().UpgradeShootingRate();
                break;
            case UpgradesTypesEnum.UPGRADEDMG:
                GetComponent<ShootingManager>().UpgradePlayerDmg();
                break;
        }
    }
}
