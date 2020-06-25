using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    [Header("Items")]
    public Item currentItem;
    public List<Item> items = new List<Item>();

    [Header("Consumables")]
    public int numberOfKeys = 0;
    public int coins = 0;
    public float maxMagic = 10;
    public float currentMagic = 0;


    private void OnEnable()
    {
        currentMagic = maxMagic;
    }


    public void ConsumeMagic(float amount)
    {
        // TODO: should you test here if you have enough magic?
        currentMagic -= amount;
    }


    public void ReceiveMagic(float amount)
    {
        currentMagic += amount;
    }


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


    public bool CheckForItem(Item item)
    {
        return items.Contains(item);
    }
}
