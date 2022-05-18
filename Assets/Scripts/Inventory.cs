using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject cluePanel;
    public List<Clue> clues = new List<Clue>();

    public void AddKey() {
        GetComponent<GameManager>().AddKey();
    }

    public void AddClue(Clue clue)
    {
        if (clues.Contains(clue))
        {
            Debug.Log("This clue is already in the list");
            return;
        }
        Debug.Log("clue: " + clue.clueTitle);
        clue.Initialise(cluePanel, inventoryUI);
        clues.Add(clue);
        Debug.Log("Clue added");
    }

    public void OpenInventoryEvent()
    {
        inventoryUI.SetActive(true);
    }
    public void ExitInventoryEvent()
    {
        inventoryUI.SetActive(false);
    }
}
