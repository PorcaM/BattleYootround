using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnProcessor : MonoBehaviour
{
    public YootPlayer owner;
    public enum ProcessState { WaitTurn, WaitThrow, WaitHorse, WaitField, End, WaitBattle }
    private ProcessState currentState;
    public YootThrowManager yootThrowManager;
    public YootGame.YootCount yootCount;
    public Horse selectedHorse;
    public YootField highlitedField;

    public ProcessState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
            owner.yootGame.gameStateUI.UpdateUI(currentState);
        }
    }

    public void StartTurn()
    {
        DecoTurnStart.ShowStarter(owner.playerID);
        yootThrowManager.StartThrow();
        CurrentState = ProcessState.WaitThrow;
    }

    public void SelectHorse(Horse horse)
    {
        if (CurrentState == ProcessState.WaitHorse)
        {
            selectedHorse = horse;
            highlitedField = YootBoard.GetDestination(horse, yootCount);
            highlitedField.DestFlag = true;
            CurrentState = ProcessState.WaitField;
        }
        else if (CurrentState == ProcessState.WaitField)
        {
            highlitedField.DestFlag = false;
            CurrentState = ProcessState.WaitHorse;
            SelectHorse(horse);
        }
    }

    public void SelectField(YootField field)
    {
        if (CurrentState == ProcessState.WaitField)
        {
            if (field == highlitedField)
            {
                highlitedField.DestFlag = false;
                MoveHorse();
                if(CurrentState != ProcessState.WaitBattle)
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
        }
        else
            CurrentState = ProcessState.WaitHorse;
    }

    private void MoveHorse()
    {
        selectedHorse.Move(yootCount);
    }

    private void EndTurn()
    {
        selectedHorse = null;
        CurrentState = ProcessState.WaitTurn;
        owner.yootGame.EndTurn(owner.playerID);
    }
}
