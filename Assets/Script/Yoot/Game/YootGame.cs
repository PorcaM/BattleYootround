using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootGame : MonoBehaviour {
    public enum GameMode { Solo, Network, Local };
    public GameMode gameMode;
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };
    public TurnManager turnManager;
    public GameObject enemyHorse;

    void Awake()
    {
        Screen.SetResolution(720, 1280, true);
    }

    void Start()
    {
        HorseRoute.Init();
    }

    public void Init()
    {

    }

    public void TestEnemyHorse()
    {
        GameObject horse = Instantiate(enemyHorse) as GameObject;
        YootBoard.fieldObjs[10].GetComponent<YootField>().Arrive(horse.GetComponent<Horse>());
    }

    void Update()
    {
        if (gameMode == GameMode.Solo)
        {
            if (turnManager.currentState == TurnManager.ProcessState.WaitTurn)
                turnManager.StartTurn();
        }
    }
}
