using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotManager : MonoBehaviour
{
    [SerializeField]
    private List<Hotspot> hotspots = new List<Hotspot>();
    [SerializeField]
    private List<Hotspot> foundHotspots = new List<Hotspot>();

    public Inventory inventory;

    void Awake() {
        InititaliseHotSpots();
    }


    public void AssignInventory(Inventory i)
    {
        inventory = i;
    }

    public void AddKey() {
        inventory.AddKey();
    }


    public void InititaliseHotSpots()
    {
        // First, shuffle the list so it is in a random order
        for (int i = 0; i < hotspots.Count; i++)
        {
            Hotspot hs = hotspots[i];
            hs.SetHotspotManager(this);
            //int randomIndex = Random.Range(0, hotspots.Count);
            //hotspots[randomIndex] = hs;
        }
    }

    public void ClueFound()
    {
        Hotspot hs = hotspots[0];
        // Add first clue to inventory
        Clue c = hs.GetClue();
        if (!c)
        {
            Debug.LogWarning("Clue from hotspot has returned null. why?");
        }
        inventory.AddClue(c);
        // If hotspot area indicator is hidden, unhide.
        // Else, reduce area indicator radius.
        if (!hs.areaIndicator.activeSelf)
        {
            Debug.Log("Unhiding area indicator?");
            hs.unHide();
        }
        else
        {
            hs.GenerateAreaIndicator(-1);
        }
        // If hotspot.clues is empty,
        // PopHotSpot
        if (hs.clues.Count == 0)
            PopHotSpot(hs);
    }

    public void PopHotSpot(Hotspot hs)
    {
        if (!foundHotspots.Contains(hs))
            foundHotspots.Add(hs);
        if (hotspots.Contains(hs))
            hotspots.Remove(hs);
    }
}
