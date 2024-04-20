using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[System.Serializable]
public class UiLocations
{
    public RectTransform position;
    public bool isOccupied;
    public ShootingTypesEnum type;
}
public class PowerUpUIHandler : MonoBehaviour
{
    [SerializeField] private RectTransform SpectralPowerUp;
    [SerializeField] private RectTransform TripleShotPowerUp;
    [SerializeField] private RectTransform BaseLocation;
    [SerializeField] private List<UiLocations> locations;
    public float speed = 8f;
    // Start is called before the first frame update

    public void MovePowerUpDown(ShootingTypesEnum type)
    {
        RectTransform movingElement;
        if (type == ShootingTypesEnum.CONE)
            movingElement = TripleShotPowerUp;
        else
            movingElement = SpectralPowerUp;
        RectTransform targetElement = getNextFreeLocation(type);
        if (!targetElement)
            return;
        StartCoroutine(MoveElementToTarget(movingElement, targetElement));
    }
    
    public void MovePowerBackUp(ShootingTypesEnum type)
    {
        RectTransform targetToMove = getCurrentLocationOfRect(type);
        if (targetToMove)
            StartCoroutine(MoveElementToTarget(targetToMove, BaseLocation));
    }
    
    private IEnumerator MoveElementToTarget(RectTransform movingElement, RectTransform targetElement)
    {
        while (Vector2.Distance(movingElement.anchoredPosition, targetElement.anchoredPosition) > 0.01f)
        {
            movingElement.anchoredPosition = Vector2.Lerp(movingElement.anchoredPosition, targetElement.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }
        movingElement.anchoredPosition = targetElement.anchoredPosition;
    }

    private RectTransform getNextFreeLocation(ShootingTypesEnum type)
    {
        RectTransform ret = null;
        foreach (UiLocations obj in locations)
        {
            if (!obj.isOccupied && ret == null)
            {
                obj.isOccupied = true;
                obj.type = type;
                ret = obj.position;
            }
            else if (obj.type == type)
            {
                return null;
            }
        }
        return ret;
    }
    
    private RectTransform getCurrentLocationOfRect(ShootingTypesEnum type)
    {
        RectTransform res = null;
        foreach (UiLocations obj in locations)
        {
            if (obj.type == type)
            {
                if (obj.type == ShootingTypesEnum.CONE)
                {
                    res = TripleShotPowerUp;
                }
                else
                {
                    res = SpectralPowerUp;
                }

                obj.isOccupied = false;
                obj.type = ShootingTypesEnum.BASE;
                return res;
            }
        }
        return null;
    }
}
