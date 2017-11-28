using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnProcessor : MonoBehaviour
{
    public YootPlayer owner;
    public enum ProcessState { Wait, Throw, Horse, Ack, End }
    public ProcessState currentState;
    public ThrowProcessor yootThrowManager;
    public GameObject TurnNetwork;      // TODO

    [SerializeField] private YootGame.YootCount yootCount;
    [SerializeField] private Horse selectedHorse;
    [SerializeField] private PopupPreview lastPreview;
    
    public void UpdateState(ProcessState nextState)
    {
        currentState = nextState;
        owner.yootGame.gameStateUI.SetValue(currentState);
    }

    public void StartTurn()
    {
        DecoTurnStart.ShowStarter(owner.playerID);
        Throw();
    }

    private void Throw()
    {
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
        lastPreview = HorseTranslator.CreatePreview(horse, yootCount, this);
        UpdateState(ProcessState.Ack);
    }

    private void HandleAnotherHorseSelect(Horse horse)
    {
        DestroyLastPreview();
        HandleHorseSelect(horse);
    }

    private void DestroyLastPreview()
    {
        if (lastPreview)
            Destroy(lastPreview.gameObject);
    }

    public void RecvAck()
    {
        if (currentState == ProcessState.Ack)
            HandleAck();
    }

    private void HandleAck()
    {
        DestroyLastPreview();
        // TODO Send horse movement to opponent
        UpdateState(ProcessState.End);
        HorseTranslator.Translate(selectedHorse, yootCount);
    }

    public void RecvEnd()
    {
        if (currentState == ProcessState.End)
            HandleEnd();
    }

    private void HandleEnd()
    {
        if (IsAgain())
            Throw();
        else
            EndTurn();
    }

    private bool IsAgain()
    {
        return yootCount == YootGame.YootCount.Yoot || yootCount == YootGame.YootCount.Mo;
    }

    public void RecvPreviewDestroy(PopupPreview preview)
    {
        if (currentState == ProcessState.Ack)
            HandlePreviewDestroy(preview);
    }

    private void HandlePreviewDestroy(PopupPreview preview)
    {
        if(preview == lastPreview)
            UpdateState(ProcessState.Horse);
    }

    private void EndTurn()
    {
        selectedHorse = null;
        lastPreview = null;
        UpdateState(ProcessState.Wait);
        owner.yootGame.EndTurn(owner.playerID);
    }
}
