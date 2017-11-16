using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public YootPlayer owner;
    public enum ProcessState { WaitTurn, WaitThrow, WaitHorse, WaitField, End };
    public ProcessState currentState;
    public YootThrowManager yootThrowManager;
    public YootGame.YootCount yootCount;
    public Horse selectedHorse;
    public YootField highlitedField;

    public void StartTurn()
    {
        yootThrowManager.StartThrow();
        currentState = ProcessState.WaitThrow;
    }

    public void SelectHorse(Horse horse)
    {
        if (currentState == ProcessState.WaitHorse)
        {
            selectedHorse = horse;
            highlitedField = YootBoard.GetDestination(horse, yootCount);
            highlitedField.DestFlag = true;
            currentState = ProcessState.WaitField;
        }
        else if (currentState == ProcessState.WaitField)
        {
            highlitedField.DestFlag = false;
            currentState = ProcessState.WaitHorse;
            SelectHorse(horse);
        }
    }

    public void SelectField(YootField field)
    {
        if (currentState == ProcessState.WaitField)
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
            currentState = ProcessState.WaitHorse;
    }

    private void MoveHorse()
    {
        selectedHorse.Move(yootCount);
        owner.JudgeGameResult();
    }

    private void EndTurn()
    {
        selectedHorse = null;
        currentState = ProcessState.WaitTurn;
    }
}
