using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PocketSand")]
public class PocketSand : Ability
{
    private int minutes = 1;

    public override void Initialise(AbilityManager ab)
    {
        base.Initialise(ab);
    }

    public override void TriggerAbility()
    {
        if (!abilityManager)
        {
            Debug.Log("No Ability Manager, could not run PocketSand");
            return;
        }
        abilityManager.PocketSandEvent(minutes);
        base.TriggerAbility();
    }

    public override void ApplyTier()
    {
        switch (upgradeTier)
        {
            case UpgradeTier.Tier2:
                minutes += 1;
                break;
            case UpgradeTier.Tier3:
                minutes += 2;
                break;
        }
    }
}
