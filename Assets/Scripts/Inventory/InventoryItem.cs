using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool unique;
    public bool usable;
    public UnityEvent useItemEvent;

    public void Use()
    {
        Debug.Log("InventoryItem: using item");
        useItemEvent.Invoke();
    }
}
