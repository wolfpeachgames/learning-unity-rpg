using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Powerup thisLoot;
    public int lootChance;
}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;


    public Powerup LootPowerup()
    {
        int cumulativeProbability = 0;
        int currentProbability = Random.Range(0, 100);
        foreach (Loot loot in loots)
        {
            cumulativeProbability += loot.lootChance;
            if (currentProbability <= cumulativeProbability)
            {
                return loot.thisLoot;
            }
        }

        return null;
    }
}
