using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the Item")]
    public InventoryItem item;

    [Header("Inventory Manager")]
    public InventoryManager inventoryManager;


    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        item = newItem;
        inventoryManager = newManager;
        if (item)
        {
            itemImage.sprite = item.itemImage;
            itemNumberText.text = item.numberHeld.ToString();
        }
    }


    public void OnClick()
    {
        if (item)
        {
            inventoryManager.SetupDescriptionAndButton(item);
        }
    }
}
