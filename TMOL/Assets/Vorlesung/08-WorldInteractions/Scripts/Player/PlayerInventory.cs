using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int itemValueToCollect = 10;
    [SerializeField] TextMeshProUGUI collectedItemsLabel;
    [SerializeField] int collectedItemsValue { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        collectedItemsValue = 0;
        UpdateHUD();
    }
    public void ItemCollected(int itemValue)
    {
        collectedItemsValue += itemValue;
        UpdateHUD();
    }
    private void UpdateHUD()
    {
        collectedItemsLabel.text = collectedItemsValue + "/" + itemValueToCollect;
    }
}
