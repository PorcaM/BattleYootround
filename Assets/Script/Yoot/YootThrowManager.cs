using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YootThrowManager : MonoBehaviour {
    public TurnManager turnManager;
    public enum ProcessState { Start, Wait };
    public ProcessState currentState;

    void Start()
    {
        currentState = ProcessState.Wait;
    }

    public void StartThrow()
    {
        currentState = ProcessState.Start;
    }

    void Update()
    {
        if (currentState == ProcessState.Start)
        {
            turnManager.RecvThrowResult(SimpleRandom());
            currentState = ProcessState.Wait;
        }
    }

    private static YootGame.YootCount SimpleRandom()
    {
        int count = Enum.GetValues(typeof(YootGame.YootCount)).Length;
        return (YootGame.YootCount)UnityEngine.Random.Range(0, count) - 1;
    }
}
