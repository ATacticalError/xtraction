using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDispenser : MonoBehaviour
{
    public Ability ability;
    public AbilityManager abilityManager;
    public bool useLootTable = true;
    public AbilityLootTable lootTable;

    void Start() {
        // FindAbilityMananger();
    }

    void FindAbilityMananger(){
        // This doesn't work on android. Why doesn't this work?
        abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        Debug.LogWarning("Looking for ability manager");
    }
    
    public void GiveAbility()
    {
        // if(!abilityManager)
        //     FindAbilityMananger();
        if(useLootTable)
            abilityManager.AddAbility(lootTable.GetRandomWeightedAbility());
        else
            abilityManager.AddAbility(ability);
    }
}
