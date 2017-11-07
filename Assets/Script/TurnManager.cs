using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum ProcessState { NotMyTurn, Throw, SelectHorse, MoveHorse, End };
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
        CurrentState = ProcessState.NotMyTurn;
    }

    void Update()
    {
        Debug.Log("CurrentState: " + currentState);
        ProceedTurn();
    }

    private void ProceedTurn()
    {
        switch (CurrentState)
        {
            case ProcessState.NotMyTurn:
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
        }
    }

    private void ThrowYoot()
    {
        yootCount = YootThrowManager.Throw();
        Debug.Log(yootCount);
        CurrentState = ProcessState.SelectHorse;
    }

    private void SelectHorse()
    {
        // wait click
    }

    public void GoMoveState()
    {
        CurrentState = ProcessState.MoveHorse;
    }

    private void MoveHorse()
    {
        selectedHorse.Move(yootCount);
    }

    private void EndTurn()
    {
        SelectedHorse = null;
        CurrentState = ProcessState.NotMyTurn;
    }
}
