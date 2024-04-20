using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[Serializable]
struct UIElements
{
    public int currentIndex;
    public List<GameObject> upgradeSteps;
    public GameObject coverObject;
}

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private FloatVariable playerHealth;
    [SerializeField] private FloatVariable shootRate;
    [SerializeField] private FloatVariable playerDmg;
    [SerializeField] private List<UIElements> _uiElementsList;
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
                UIUpdate(2);
                break;
            case UpgradesTypesEnum.UPGRADEBULLETSPEED:
                GetComponent<ShootingManager>().UpgradeShootingRate();
                UIUpdate(0);
                break;
            case UpgradesTypesEnum.UPGRADEDMG:
                UIUpdate(1);
                GetComponent<ShootingManager>().UpgradePlayerDmg();
                break;
        }
    }

    private void UIUpdate(int idx)
    {
        UIElements currentUpgrade = _uiElementsList[idx];
        if (currentUpgrade.currentIndex > 2)
        {
            return;
        }
        _uiElementsList[idx].upgradeSteps[currentUpgrade.currentIndex].SetActive(true);
        currentUpgrade.currentIndex += 1;
        if (currentUpgrade.currentIndex == 3)
        {
            currentUpgrade.coverObject.SetActive(true);
        }
        _uiElementsList[idx] = currentUpgrade;
        CheckAllUpgrades();
    }

    private void CheckAllUpgrades()
    {
        int count = 0;
        foreach (var obj in _uiElementsList)
        {
            if (obj.currentIndex == 3)
            {
                count++;
            }
        }
        if (count == 3)
        {
            GameManager.Instance.shouldTrainStop = false;
        }
    }
}
