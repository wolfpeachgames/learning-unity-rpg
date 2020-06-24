using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [Header("Loot")]
    public LootTable lootTable;


    public void MakeLoot()
    {
        if (lootTable != null)
        {
            Powerup current = lootTable.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
