using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Abilities/Invisibility")]
public class Invisibility : Ability
{
    private float invisibleSeconds = 5.0f;

    public override void Initialise(AbilityManager ab)
    {
        base.Initialise(ab);
        abilityButton.onClick.AddListener(TriggerAbility);
        ApplyTier();
    }

    public override void TriggerAbility()
    {
        if(abilityManager)
            abilityManager.InvisibleEvent(invisibleSeconds);
        else
            Debug.Log("No Ability Manager, could not run Invisibility");
    }

    void ApplyTier()
    {
        switch (upgradeTier)
        {
            case UpgradeTier.Tier2:
                invisibleSeconds += 5.0f;
                break;
            case UpgradeTier.Tier3:
                invisibleSeconds += 10.0f;
                break;
        }
    }
}
