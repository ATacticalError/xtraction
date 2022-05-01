using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Button InvisibleButton;
    public float invisibleSeconds = 5.0f;
    public Button BlindspotButton;
    public float blindspotPercentage = 0.2f;
    public Button PocketSandButton;
    public int pocketsandMinutes = 1;
    public Button RoadworkButton;
    public int roadworkMinutes = 1;
    public Button AssassinButton;

    public MapManager mapManager;

    public Player player;
    public Material playerMat;
    [Range(0, 1)]
    public float playerInvisibleOpacity = 0.75f;
    private Color playerColor;

    void Awake()
    {
        player = this.GetComponent<Player>();
        playerColor = playerMat.color;
        playerColor.a = 1.0f;
        playerMat.color = playerColor;
    }

    public void SetMapManager(MapManager manager) { mapManager = manager; }

    public IEnumerator PlayerInvisibilityRoutine(float seconds)
    {
        playerColor.a = playerInvisibleOpacity;
        playerMat.color = playerColor;
        yield return new WaitForSeconds(seconds);
        playerColor.a = 1.0f;
        playerMat.color = playerColor;
    }


    public void InvisibleEvent()
    {
        StartCoroutine(PlayerInvisibilityRoutine(invisibleSeconds));
        if (!mapManager)
            return;
        mapManager.ApplyInvisibility(invisibleSeconds);
        Debug.Log("Applying Invisiblity");
    }

    public void BlindspotEvent()
    {
        if (!mapManager)
            return;
        mapManager.ApplyBlindspot(blindspotPercentage);
        Debug.Log("Applying Blindspot");
    }

    public void PocketSandEvent()
    {
        if (!mapManager)
            return;
        mapManager.ApplyPocketSand(pocketsandMinutes);
        Debug.Log("Applying Pocket Sand");
    }

    public void RoadworkEvent()
    {
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
        mapManager.ApplyRoadwork(roadworkMinutes, patrolPoint);
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
