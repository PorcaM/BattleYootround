﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum ProcessState { WaitTurn, WaitThrow, WaitHorse, WaitField, End };

    public YootThrowManager yootThrowManager;

    public ProcessState currentState;
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

    public void StartTurn()
    {
        yootThrowManager.StartThrow();
        CurrentState = ProcessState.WaitThrow;
    }

    private void ProceedTurn()
    {
        switch (CurrentState)
        {
            case ProcessState.End:
                EndTurn();
                break;
            default:
            case ProcessState.WaitTurn:
            case ProcessState.WaitThrow:
            case ProcessState.WaitHorse:
            case ProcessState.WaitField:
                break;
        }
    }

    public void HorseIsSelected(Horse horse)
    {
        if (CurrentState == ProcessState.WaitHorse)
        {
            selectedHorse = horse;
            highlitedField = YootBoard.GetDestination(horse, yootCount);
            highlitedField.DestFlag = true;
            CurrentState = ProcessState.WaitField;
        }
        else if (currentState == ProcessState.WaitField)
        {
            highlitedField.DestFlag = false;
            CurrentState = ProcessState.WaitHorse;
            HorseIsSelected(horse);
        }
    }

    public void FieldIsSelected(YootField field)
    {
        if (CurrentState == ProcessState.WaitField)
        {
            if (field == highlitedField)
            {
                highlitedField.DestFlag = false;
                MoveHorse();
                EndTurn();
            }
        }
    }

    public void RecvThrowResult(YootGame.YootCount yootCount)
    {
        this.yootCount = yootCount;
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
        //foreach (Horse horse in SelectedHorse.currentLocation.horses)
        //{
        //    horse.Move(yootCount);
        //}
        SelectedHorse.Move(yootCount);
    }

    private void EndTurn()
    {
        SelectedHorse = null;
        CurrentState = ProcessState.WaitTurn;
    }
}
