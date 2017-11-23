using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnProcessor : MonoBehaviour
{
    public YootPlayer owner;
    public enum ProcessState { WaitTurn, Throw, Horse, Ack, End, Battle }
    public ProcessState currentState;
    public YootThrowManager yootThrowManager;

    [SerializeField] private YootGame.YootCount yootCount;
    [SerializeField] private Horse selectedHorse;
    private PopupPreview lastPreview;

    private void UpdateState(ProcessState nextState)
    {
        currentState = nextState;
        owner.yootGame.gameStateUI.SetValue(currentState);
    }

    public void StartTurn()
    {
        DecoTurnStart.ShowStarter(owner.playerID);
        yootThrowManager.StartThrow();
        UpdateState(ProcessState.Throw);
    }

    public void RecvThrowResult(YootGame.YootCount yootCount)
    {
        if (currentState == ProcessState.Throw)
            HandleThrowResult(yootCount);
    }

    private void HandleThrowResult(YootGame.YootCount yootCount)
    {
        this.yootCount = yootCount;
        if (yootCount == YootGame.YootCount.Nak)
            EndTurn();
        else
            UpdateState(ProcessState.Horse);
    }

    public void RecvHorseSelect(Horse horse)
    {
        if (currentState == ProcessState.Horse)
            HandleHorseSelect(horse);
        else if (currentState == ProcessState.Ack)
            HandleAnotherHorseSelect(horse);
    }

    private void HandleHorseSelect(Horse horse)
    {
        selectedHorse = horse;
        YootField dest = YootBoard.GetDestination(horse, yootCount);
        lastPreview = PopupPreviewController.CreatePopupPreview("dest", dest.transform, this);
        UpdateState(ProcessState.Ack);
    }

    private void HandleAnotherHorseSelect(Horse horse)
    {
        if (lastPreview)
            Destroy(lastPreview.gameObject);
        HandleHorseSelect(horse);
    }

    public void RecvAck()
    {
        if (currentState == ProcessState.Ack)
            HandleAck();
    }

    private void HandleAck()
    {
        selectedHorse.Move(yootCount);
    }

    private void EndTurn()
    {
        selectedHorse = null;
        lastPreview = null;
        UpdateState(ProcessState.WaitTurn);
        owner.yootGame.EndTurn(owner.playerID);
    }
}
