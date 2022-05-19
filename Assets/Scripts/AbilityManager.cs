using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public LayoutGroup AbilityTray;
    public int abilityMaxCount = 3;
    [SerializeField]
    private List<Ability> currentAbilities;
    private int rwMinutes;

    public MapManager mapManager;

    public Player player;
    public Material playerMat;
    [Range(0, 1)]
    public float playerInvisibleOpacity = 0.75f;
    private Color playerColor;

    public GameObject[] abilityISlot;
    private int abilityISlotCounter = 0;

    void Awake()
    {
        player = this.GetComponent<Player>();
        playerColor = playerMat.color;
        playerColor.a = 1.0f;
        playerMat.color = playerColor;
    }

    public GameObject GetAbilityIconSlot(){
        GameObject aIcon = abilityISlot[abilityISlotCounter];
        if(abilityISlotCounter < abilityISlot.Length)
            abilityISlotCounter++;
        else
            abilityISlotCounter = 0;
        return aIcon;
    }

    public void SetMapManager(MapManager manager) { mapManager = manager; }

    public void AddAbility(Ability ability)
    {
        if (currentAbilities.Count >= abilityMaxCount)
        {
            //TODO: Show swap ability dialogue
        }
        ability.Initialise(this);
        currentAbilities.Add(ability);
    }

    public void AddRandomAbility() {
        if(TryGetComponent<AbilityDispenser>(out AbilityDispenser ad)) {
            print("Found ability dispenser");
            ad.GiveAbility();
        }
    }

    public void RemoveAbility(Ability ab)
    {
        if (currentAbilities.Contains(ab))
        {
            currentAbilities.Remove(ab);
        }
    }

    public IEnumerator PlayerInvisibilityRoutine(float seconds)
    {
        playerColor.a = playerInvisibleOpacity;
        playerMat.color = playerColor;
        yield return new WaitForSeconds(seconds);
        playerColor.a = 1.0f;
        playerMat.color = playerColor;
    }

    public void InvisibleEvent(float invisibleSeconds)
    {
        StartCoroutine(PlayerInvisibilityRoutine(invisibleSeconds));
        if (!mapManager)
            return;
        mapManager.ApplyInvisibility(invisibleSeconds);
        Debug.Log("Applying Invisiblity");
    }

    public void BlindspotEvent(float blindspotPercentage)
    {
        if (!mapManager)
            return;
        mapManager.ApplyBlindspot(blindspotPercentage);
        Debug.Log("Applying Blindspot");
    }

    public void PocketSandEvent(int pocketsandMinutes)
    {
        if (!mapManager)
            return;
        mapManager.ApplyPocketSand(pocketsandMinutes);
        Debug.Log("Applying Pocket Sand");
    }

    public void RoadworkEvent(int roadworkMinutes)
    {
        rwMinutes = roadworkMinutes;
        player.selectionType = SelectionType.Roadwork;
        Debug.Log("Applying Roadwork");
    }

    public void AssassinEvent()
    {
        if (!mapManager)
            return;
        player.selectionType = SelectionType.Assassin;
        Debug.Log("Applying Assassin");
    }

    public void Roadwork(GameObject patrolPoint)
    {
        if (!mapManager)
            return;
        mapManager.ApplyRoadwork(rwMinutes, patrolPoint);
        player.selectionType = SelectionType.Movement;
    }

    public void Assassinate(Patrol patrol)
    {
        if (!mapManager)
            return;
        mapManager.RemovePatrol(patrol);
        player.selectionType = SelectionType.Movement;
    }
}
