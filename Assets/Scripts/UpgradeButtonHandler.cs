using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonHandler : MonoBehaviour
{
    [SerializeField] private UpgradesTypesEnum type;
    [SerializeField] private OnUpgradeChoice upgradeEvent;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => RaiseEvent(type));
    }

    private void RaiseEvent(UpgradesTypesEnum type)
    {
        upgradeEvent.Raise(type);
        canvas.SetActive(false);
    }
}
