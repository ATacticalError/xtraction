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

    public MapManager mapManager;
    public float patrolSpeedMultiplier;

    void Update()
    {
        if(mapManager) {
            
        }
    }

    public void AddKey()
    {
        keys++;
        keysText.text = ("Keys Found: " + keys);
        if (keys >= 4)
        {
            CompleteGame();
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
