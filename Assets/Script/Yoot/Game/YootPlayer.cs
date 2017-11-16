using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootPlayer : MonoBehaviour {
    private const int numHorse = 4;

    public HorseFactory horseFactory;
    public HorseManager horseManager;
    public TurnManager turnManager;
    public UIHandler uiHandler;
    public string tagPrefix;
    public int playerID;

    public void Init()
    {
        if (YootBoard.isReady)
            horseManager.SetupHorses();
    }

    public void StartNewHorse()
    {
        horseManager.StartNewHorse();        
    }

    public void JudgeGameResult()
    {
        if (horseManager.IsGameOver())
            Win();
    }

    private void Win()
    {
        Debug.Log("Player " + playerID + " Win!!");
    }

    public void Register(Horse horse)
    {

    }
}
