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
        StartCoroutine(MoveElementToTarget(movingElement, targetElement));
    }
    
    public void MovePowerBackUp(ShootingTypesEnum type)
    {
        targetToMove = null;
        int res = getCurrentLocationOfRect(type);
        if (targetToMove && res != -1 && !locations[res].isMoving)
            StartCoroutine(MoveElementToTargetUp(targetToMove, BaseLocation));
    }
    
    private IEnumerator MoveElementToTarget(RectTransform movingElement, RectTransform targetElement)
    {
        while (Vector2.Distance(movingElement.anchoredPosition, targetElement.anchoredPosition) > 0.01f)
        {
            movingElement.anchoredPosition = Vector2.Lerp(movingElement.anchoredPosition, targetElement.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }
        movingElement.position = targetElement.position;
    }
    
    private IEnumerator MoveElementToTargetUp(RectTransform movingElement, RectTransform targetElement)
    {
        while (Vector2.Distance(movingElement.anchoredPosition, targetElement.anchoredPosition) > 0.01f)
        {
            movingElement.anchoredPosition = Vector2.Lerp(movingElement.anchoredPosition, targetElement.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }
        movingElement.position = targetElement.position;
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
    
    private int getCurrentLocationOfRect(ShootingTypesEnum type)
    {
        for (int i = 0; i < locations.Count; i++)
        {
            if (locations[i].type == type)
            {
                if (locations[i].type == ShootingTypesEnum.CONE)
                {
                    targetToMove = TripleShotPowerUp;
                }
                else
                {
                    targetToMove = SpectralPowerUp;
                }

                locations[i].isOccupied = false;
                locations[i].type = ShootingTypesEnum.BASE;
                return i;
            }
        }
        return -1;
    }
}
