using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private FloatVariable playerHealth;
    [SerializeField] private FloatVariable bulletSpeed;
    void Start()
    {
        GetComponent<OnUpgradeChoiceListener>().OnPowerUpActivation += EventTrigger;
    }

    public void EventTrigger(UpgradesTypesEnum type)
    {
        Debug.Log("Upgrade " + type + " chosen");
        switch (type)
        {
            case UpgradesTypesEnum.UPGRADEHEALTH:
                playerHealth.value += 20;
                break;
            case UpgradesTypesEnum.UPGRADEBULLETSPEED:
                bulletSpeed.value += 10;
                break;
        }
    }
}
