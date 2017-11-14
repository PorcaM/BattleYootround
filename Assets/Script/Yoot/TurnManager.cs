using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum ProcessState { WaitTurn, Throw, WaitHorse, WaitField, End };
    private ProcessState currentState;
    private YootGame.YootCount yootCount;
    private Horse selectedHorse;

    private YootField highlitedField;

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
            highlitedField = YootBoard.GetDestination(horse, yootCount);
            highlitedField.Highlight(true);
            CurrentState = ProcessState.WaitField;
        }
        else if (currentState == ProcessState.WaitField)
        {
            highlitedField.Highlight(false);
            CurrentState = ProcessState.WaitHorse;
            HorseIsSelected(horse);
        }
    }

    public void FieldIsSelected(YootField field)
    {
        if (CurrentState == ProcessState.WaitField)
        {
            highlitedField.Highlight(false);
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
