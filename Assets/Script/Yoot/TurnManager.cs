using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum ProcessState { WaitTurn, Throw, WaitHorse, WaitField, End };
    private ProcessState currentState;
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
            case ProcessState.WaitHorse:
            case ProcessState.WaitField:
                break;
            case ProcessState.End:
                EndTurn();
                break;
            default:
                break;
        }
    }

    public void HorseIsSelected(Horse horse)
    {
        if (CurrentState == ProcessState.WaitHorse)
        {
            selectedHorse = horse;
            YootField destination = YootBoard.GetDestination(horse, yootCount);
            destination.WaitSelect();
            CurrentState = ProcessState.WaitField;
        }
    }

    public void FieldIsSelected(YootField field)
    {
        if (CurrentState == ProcessState.WaitField)
        {
            field.NotWaitSelect();
            MoveHorse();
            EndTurn();
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
            CurrentState = ProcessState.WaitHorse;
    }

    private void MoveHorse()
    {
        SelectedHorse.Move(yootCount);
    }

    private void EndTurn()
    {
        SelectedHorse = null;
        CurrentState = ProcessState.WaitTurn;
    }
}
