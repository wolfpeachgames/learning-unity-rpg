using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryContentsPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;


    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        useButton.SetActive(buttonActive);
    }


    private void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                GameObject blankItemSlot = Instantiate(blankInventorySlot, inventoryContentsPanel.transform.position, Quaternion.identity);
                blankItemSlot.transform.SetParent(inventoryContentsPanel.transform);
                InventorySlot newSlot = blankItemSlot.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.items[i], this);
                }
            }
        }
    }


    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false); // initialize as blank
    }


    void Update()
    {
        
    }
}
