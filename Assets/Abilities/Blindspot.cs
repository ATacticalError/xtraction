using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Blindspot")]
public class Blindspot : Ability
{
    private float percentage = 0.2f;

    public override void Initialise(AbilityManager ab)
    {
        base.Initialise(ab);
        abilityButton.onClick.AddListener(TriggerAbility);
        ApplyTier();
    }

    public override void TriggerAbility()
    {
        if (!abilityManager)
        {
            Debug.Log("No Ability Manager, could not run Blindspot");
            return;
        }
        abilityManager.BlindspotEvent(percentage);
        currentCount--;
        if (singleUse && currentCount <= 0)
        {
            abilityButtonObject.SetActive(false);
            abilityManager.RemoveAbility(this);
        }
    }

    void ApplyTier()
    {
        switch (upgradeTier)
        {
            case UpgradeTier.Tier2:
                percentage += 0.2f;
                break;
            case UpgradeTier.Tier3:
                percentage += 0.4f;
                break;
        }
    }
}
