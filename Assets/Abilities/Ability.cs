using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ScriptableObject
{
    public string abilityName = "AbilityName";
    public string abilityDescription = "AbilityDescription";
    public UpgradeTier upgradeTier;
    public GameObject abilityButtonPrefab;
    public GameObject abilityButtonObject;
    public Button abilityButton;
    public Text abilityButtonText;
    public Sprite icon;
    public GameObject model;
    public AbilityManager abilityManager;
    public bool singleUse;
    public int maxStack = 3;
    public int currentCount = 1;

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
    }
    public abstract void TriggerAbility();
}
