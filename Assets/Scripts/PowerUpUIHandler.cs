using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UiLocations
{
    public RectTransform position;
    public bool isOccupied;
    public bool isMoving;  // Ensures that no two movements happen at the same time on the same element
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
    private Dictionary<RectTransform, Coroutine> activeCoroutines = new Dictionary<RectTransform, Coroutine>();

    private float timeBeforeCorrection = 3f;
    private float currentTimeBeforeCorrection = 0f;

    private void Update()
    {
        currentTimeBeforeCorrection += Time.deltaTime;
        if (currentTimeBeforeCorrection >= timeBeforeCorrection)
        {
            if (CheckIfAllFree())
            {
                var anchoredPosition = BaseLocation.anchoredPosition;
                SpectralPowerUp.anchoredPosition = anchoredPosition;
                TripleShotPowerUp.anchoredPosition = anchoredPosition;

                // Force the RectTransform to update its layout immediately
                LayoutRebuilder.ForceRebuildLayoutImmediate(SpectralPowerUp);
                LayoutRebuilder.ForceRebuildLayoutImmediate(TripleShotPowerUp);
            }

            currentTimeBeforeCorrection = 0f;
        }
    }

    public bool CheckIfAllFree()
    {
        foreach (UiLocations obj in locations)
        {
            if (obj.isOccupied)
            {
                return false;
            }
        }
        return true;
    }

    public void MovePowerUpDown(ShootingTypesEnum type)
    {
        RectTransform movingElement = (type == ShootingTypesEnum.CONE) ? TripleShotPowerUp : SpectralPowerUp;
        RectTransform targetElement = getNextFreeLocation(type);
        if (targetElement != null)
        {
            StartCoroutine(MoveElementToTarget(movingElement, targetElement));
        }
    }
    
    public void MovePowerBackUp(ShootingTypesEnum type)
    {
        int res = getCurrentLocationOfRect(type);
        if (targetToMove != null && res != -1 && !locations[res].isMoving)
        {
            StartCoroutine(MoveElementToTarget(targetToMove, BaseLocation));
        }
    }
    
    private IEnumerator MoveElementToTarget(RectTransform movingElement, RectTransform targetElement)
    {
        if (activeCoroutines.TryGetValue(movingElement, out Coroutine existingCoroutine))
        {
            StopCoroutine(existingCoroutine);
            activeCoroutines.Remove(movingElement);
        }

        Coroutine newCoroutine = StartCoroutine(PerformMovement(movingElement, targetElement));
        activeCoroutines[movingElement] = newCoroutine;
        yield return newCoroutine;
        activeCoroutines.Remove(movingElement);
    }

    private IEnumerator PerformMovement(RectTransform movingElement, RectTransform targetElement)
    {
        UiLocations location = locations.Find(loc => loc.position == movingElement);
        if (location != null) location.isMoving = true;

        while (Vector2.Distance(movingElement.anchoredPosition, targetElement.anchoredPosition) > 0.01f)
        {
            movingElement.anchoredPosition = Vector2.Lerp(movingElement.anchoredPosition, targetElement.anchoredPosition, speed * Time.deltaTime);
            yield return null;
        }
        movingElement.anchoredPosition = targetElement.anchoredPosition;

        if (location != null) location.isMoving = false;
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
                    targetToMove = TripleShotPowerUp;
                else if (locations[i].type == ShootingTypesEnum.SPECTRAL)
                    targetToMove = SpectralPowerUp;

                locations[i].isOccupied = false;
                locations[i].type = ShootingTypesEnum.BASE;
                return i;
            }
        }
        return -1;
    }
}
