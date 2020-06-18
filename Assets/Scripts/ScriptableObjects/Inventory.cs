using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;


    public void AddItem(Item itemToAdd)
    {
        // is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        // else it is an item to store in the inventory
        else
        {
            if (!items.Contains(itemToAdd)) // forces items to be unique
            {
                items.Add(itemToAdd);
            }
        }
    }
}
