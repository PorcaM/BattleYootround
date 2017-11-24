using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootPlayer : MonoBehaviour {
    private const int numHorse = 4;

    public YootGame yootGame;
    public HorseFactory horseFactory;
    public HorseManager horseManager;
    public TurnProcessor turnProcessor;
    public string tagPrefix;
    public int playerID;

    public void Init()
    {
        horseManager.Init();
    }

    public void Win()
    {
        yootGame.HandleWin(playerID);
    }
}
