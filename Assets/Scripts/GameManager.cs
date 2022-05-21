using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameComplete = false;

    public GameObject gameCompleteUI;

    public Text gameOverCounterText;
    public int gameOvers = 0;
    public Text keysText;
    public int keys = 0;
    public int clues;

    public GameObject[] keyDialogue;
    public GameObject[] clueDialogue;

    public MapManager mapManager;
    public float patrolSpeedMultiplier;

    public GameObject canvas;

    void Update()
    {
        if (mapManager)
        {

        }
    }

    public void AddKey()
    {
        keys++;
        keysText.text = ("Keys Found: " + keys);

        switch (keys)
        {
            case 1:
                SpawnDialogue(keyDialogue[0]);
                break;
            case 2:
                SpawnDialogue(keyDialogue[1]);
                break;
            case 3:
                SpawnDialogue(keyDialogue[2]);
                break;
            case 4:
                SpawnDialogue(keyDialogue[3]);
                break;
        }
        if (keys >= 4)
        {
            CompleteGame();
        }
    }

    public void SpawnDialogue(GameObject dialogue)
    {
        GameObject d = GameObject.Instantiate(dialogue, canvas.transform);
        d.SetActive(true);
    }

    public void AddClue()
    {
        clues++;
        switch (clues)
        {
            case 2:
                SpawnDialogue(clueDialogue[0]);
                break;
            case 4:
                SpawnDialogue(clueDialogue[1]);
                break;
            case 6:
                SpawnDialogue(clueDialogue[2]);
                break;
        }
    }

    public void AddGameOver()
    {
        gameOvers++;
        gameOverCounterText.text = ("GameOvers: " + gameOvers);
    }

    public void CompleteGame()
    {
        gameCompleteUI.SetActive(true);
        Time.timeScale = 0.1f;
        gameComplete = true;
    }
}
