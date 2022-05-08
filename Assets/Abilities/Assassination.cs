using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Assassination")]
public class Assassination : Ability
{
    public override void Initialise(AbilityManager ab)
    {
        base.Initialise(ab);
    }

    public override void TriggerAbility()
    {
        if (!abilityManager)
        {
            Debug.Log("No Ability Manager, could not run Assassination");
            return;
        }
        abilityManager.AssassinEvent();
        base.TriggerAbility();
    }

    public override void ApplyTier() { }
}
