using System;
using System.Collections;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UpgradesTypesEnum type;
    [SerializeField] private OnUpgradeChoice upgradeEvent;
    [SerializeField] private GameObject highlight;
    [SerializeField] private Sprite highlightSprite;
    [SerializeField] private Sprite baseSprite;
    public bool isDeactivated;

   
    public bool isFullyUpgraded;

    void Start()
    {
        baseSprite = GetComponent<Image>().sprite;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDeactivated)
            return;
        highlight.SetActive(true);
        GetComponent<Image>().sprite = highlightSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDeactivated)
            return;
        highlight.SetActive(false);
        GetComponent<Image>().sprite = baseSprite;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDeactivated)
            return;
        highlight.SetActive(false);
        GetComponent<Image>().sprite = baseSprite;
        upgradeEvent.Raise(type);
        GameManager.Instance.StartGameAgain();
        StartCoroutine(makeButtonsUnclickable());
    }

    IEnumerator makeButtonsUnclickable()
    {
        isDeactivated = true;
        yield return new WaitForSeconds(2f);
        isDeactivated = false;
    }
}
