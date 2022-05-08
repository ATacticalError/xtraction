using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDispenser : MonoBehaviour
{
    public Ability ability;
    public AbilityManager abilityManager;
    public AbilityLootTable lootTable;

    void Start() {
        abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }

    public void GiveAbility()
    {
        abilityManager.AddAbility(lootTable.GetRandomWeightedAbility());
    }
}
