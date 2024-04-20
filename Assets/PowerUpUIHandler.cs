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
    public bool isMoving;
    public ShootingTypesEnum type;
}
public class PowerUpUIHandler : MonoBehaviour
{
    [SerializeField] private RectTransform SpectralPowerUp;
    [SerializeField] private RectTransform TripleShotPowerUp;
    [SerializeField] private RectTransform BaseLocation;
    [SerializeField] private List<UiLocations> locations;
    public float speed = 8f;

    private RectTransform targetToMove;
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
        StartCoroutine(MoveElementToTarget(movingElement, targetElement, null));
    }
    
    public void MovePowerBackUp(ShootingTypesEnum type)
    {
        targetToMove = null;
        UiLocations res = getCurrentLocationOfRect(type);
        if (targetToMove && !res.isMoving)
            StartCoroutine(MoveElementToTarget(targetToMove, BaseLocation, res));
    }
    
    private IEnumerator MoveElementToTarget(RectTransform movingElement, RectTransform targetElement, UiLocations loc)
    {
        if (loc != null)
            loc.isMoving = true;
        while (Vector2.Distance(movingElement.anchoredPosition, targetElement.anchoredPosition) > 0.01f)
        {
            movingElement.anchoredPosition = Vector2.Lerp(movingElement.anchoredPosition, targetElement.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }
        movingElement.position = targetElement.position;
        if (loc != null)
            loc.isMoving = false;
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
    
    private UiLocations getCurrentLocationOfRect(ShootingTypesEnum type)
    {
        foreach (UiLocations obj in locations)
        {
            if (obj.type == type)
            {
                if (obj.type == ShootingTypesEnum.CONE)
                {
                    targetToMove = TripleShotPowerUp;
                }
                else
                {
                    targetToMove = SpectralPowerUp;
                }

                obj.isOccupied = false;
                obj.type = ShootingTypesEnum.BASE;
                return obj;
            }
        }
        return null;
    }
}
