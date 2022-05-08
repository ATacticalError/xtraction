using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDispenser : MonoBehaviour
{
    public Ability ability;
    public AbilityManager abilityManager;

    void Start() {
        abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }

    public void GiveAbility()
    {
        abilityManager.AddAbility(ability);
    }
}
