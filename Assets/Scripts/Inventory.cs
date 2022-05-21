using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject canvas;
    public GameObject inventoryUI;
    public GameObject cluePanel;
    public List<Clue> clues = new List<Clue>();

    public GameManager gameManager;

    public void AddKey() {
        gameManager.AddKey();
    }

    public void AddClue(Clue clue)
    {
        if (clues.Contains(clue))
        {
            Debug.Log("This clue is already in the list");
            return;
        }
        Debug.Log("clue: " + clue.clueTitle);
        clue.Initialise(cluePanel, inventoryUI, canvas);
        clues.Add(clue);
        Debug.Log("Clue added");
        gameManager.AddClue();
    }

    public void SpawnDialogue(GameObject dialogue) {
        GameObject d = GameObject.Instantiate(dialogue, canvas.transform);
        d.SetActive(true);
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
