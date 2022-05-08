using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loot table for the Ability ScriptableObjects.
/// Stolen from: 
/// https://medium.com/geekculture/creating-a-basic-loot-table-for-power-ups-25a9fe2481aa
/// by Vincent Taylor
/// </summary>
[System.Serializable]
public struct AbilityLootTable
{
    public AbilityLootItem[] abilityLootItems;

    private int Total()
    {
        int t = 0;
        foreach (AbilityLootItem itm in abilityLootItems)
        {
            t += itm.chance;
        }
        return t;
    }

    public Ability GetRandomWeightedAbility()
    {
        // Random value between 0 and the total value of all items' chances.
        int randomLootValue = Random.Range(0, Total());
        // Used to check if the sampled random value is within range of an item
        int currentRangeMin = 0;

        foreach (AbilityLootItem itm in abilityLootItems)
        {
            int currentRangeMax = (currentRangeMin + itm.chance);
            // if the random value is between the cur min and max
            Debug.Log("Current range min: " + currentRangeMin + "/max: " + currentRangeMax + "randomLootValue: " + randomLootValue);
            if (randomLootValue > currentRangeMin && randomLootValue <= currentRangeMax)
            {
                return itm.ability;
            }
            // Otherwise, update the minimum and move onto next item in array
            currentRangeMin += itm.chance;
        }
        Debug.LogError("Loot table is null or empty.");
        return null;
    }

    [System.Serializable]
    public struct AbilityLootItem
    {
        public Ability ability;
        public int chance;
    }
}