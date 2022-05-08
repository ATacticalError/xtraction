using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "AbilityName";
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
        abilityButton.onClick.AddListener(TriggerAbility);
        ApplyTier();
    }

    public virtual void TriggerAbility()
    {
        if (singleUse)
        {
            DestroyAbility();
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
