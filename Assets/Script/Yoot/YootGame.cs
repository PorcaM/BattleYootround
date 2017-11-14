﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootGame : MonoBehaviour {
    public enum Mode { Solo, Network };
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };
    public TurnManager turnManager;

    private Mode mode;

    void Start()
    {
        mode = Mode.Solo;
        HorseRoute.Init();
    }

    void Update()
    {
        if (mode == Mode.Solo)
        {
            if (turnManager.CurrentState == TurnManager.ProcessState.WaitTurn)
                turnManager.StartTurn();
        }
    }

    public static void Win()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
