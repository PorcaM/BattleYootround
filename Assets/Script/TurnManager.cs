using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum ProcessState { WaitTurn, Throw, SelectHorse, MoveHorse, End };
    private ProcessState currentState;
    private GameObject[] horseObjects;
    private YootGame.YootCount yootCount;
    private Horse selectedHorse;

    public ProcessState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    public Horse SelectedHorse
    {
        get
        {
            return selectedHorse;
        }

        set
        {
            selectedHorse = value;
        }
    }

    void Start()
    {
        horseObjects = GameObject.FindGameObjectsWithTag("AllyHorse");
        CurrentState = ProcessState.WaitTurn;
    }

    void Update()
    {
        ProceedTurn();
    }

    private void ProceedTurn()
    {
        switch (CurrentState)
        {
            case ProcessState.WaitTurn:
                return;
            case ProcessState.Throw:
                ThrowYoot();
                break;
            case ProcessState.SelectHorse:
                SelectHorse();
                break;
            case ProcessState.MoveHorse:
                MoveHorse();
                break;
            case ProcessState.End:
                EndTurn();
                break;
            default:
                break;
        }
    }

    private void ThrowYoot()
    {
        yootCount = YootThrowManager.Throw();
        Debug.Log("Throw: " + yootCount);
        if (yootCount == YootGame.YootCount.Nak)
        {
            EndTurn();
            return;
        }
        else
            CurrentState = ProcessState.SelectHorse;
    }

    private void SelectHorse()
    {
        // wait click
    }

    private void MoveHorse()
    {
        SelectedHorse.Move(yootCount);
        EndTurn();
    }

    private void EndTurn()
    {
        SelectedHorse = null;
        CurrentState = ProcessState.WaitTurn;
    }
}
