using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "AbilityName";
    [TextArea(15,20)]
    public string abilityDescription = "AbilityDescription";
    public UpgradeTier upgradeTier;

    [HideInInspector] public AbilityManager abilityManager;

    public GameObject abilityButtonPrefab;
    public GameObject abilityButtonObject;
    [HideInInspector] public Button abilityButton;
    [HideInInspector] public Text abilityButtonText;

    public Sprite icon;
    public GameObject model;

    public bool singleUse;

    public Ability nextTier;

    public AbilityIconSlot iconSlot;

    public virtual void Initialise(AbilityManager ab)
    {
        abilityManager = ab;
        abilityButtonObject = GameObject.Instantiate(abilityButtonPrefab, ab.AbilityTray.transform);
        abilityButton = abilityButtonObject.GetComponent<Button>();
        abilityButtonText = abilityButtonObject.GetComponentInChildren<Text>();
        if (!abilityButton || !abilityButtonText)
        {
            Debug.Log("abilityButton Or abilityButtonText text are null");
            return;
        }
        abilityButtonText.text = abilityName;
        UnityEditor.Events.UnityEventTools.AddPersistentListener(abilityButton.onClick, TriggerAbility);
        // abilityButton.onClick.AddListener(TriggerAbility);
        ApplyTier();
        CreateButtonIcon(ab);
    }

    public virtual void CreateButtonIcon(AbilityManager ab){
        // Get ability icon slot from ability manager
        iconSlot = ab.ReserveAbilityIconSlot();
        iconSlot.AddIcon(model, upgradeTier);
        abilityButtonObject.GetComponent<RawImage>().texture = iconSlot.cam.targetTexture;
    }

    public virtual void TriggerAbility()
    {
        if (singleUse)
        {
            DestroyAbility();
            iconSlot.cam.targetTexture.Release();
            abilityManager.ClearAbilityIconSlot(iconSlot);
        }
    }

    public virtual void DestroyAbility()
    {
        abilityButtonObject.Destroy();
        abilityManager.RemoveAbility(this);
    }

    public virtual void UpgradeToNextTier()
    {
        if (nextTier)
        {
            nextTier.Initialise(abilityManager);
            DestroyAbility();
        }
    }

    public abstract void ApplyTier();
}
