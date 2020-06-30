using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PhysicalInventoryItem: collsion");
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }


    private void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            if (!playerInventory.items.Contains(thisItem))
            {
                playerInventory.items.Add(thisItem);
            }
            thisItem.numberHeld++;
        }
    }
}
