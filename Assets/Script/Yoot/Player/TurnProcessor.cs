using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnProcessor : MonoBehaviour
{
    public YootPlayer owner;
    public enum ProcessState { Wait, Throw, Horse, Ack, End }
    public ProcessState currentState;
    public ThrowProcessor yootThrowManager;
    public TurnNetworkSendProcess turnSend;
    public TurnNetworkRecvProcess turnRecv;

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
        if(YootGame.isNetwork && owner.playerID == 0)
        {
            TurnManager turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
            turnManager.ClearAll();
        }
        Throw();
    }

    private void Throw()
    {
        yootThrowManager.StartThrow();
        UpdateState(ProcessState.Throw);
    }

    public void RecvThrowResult(YootGame.YootCount yootCount)
    {
        if (YootGame.isNetwork && owner.playerID == 0)
        {
            Debug.Log("Send result yootCount: " + yootCount);
            BYMessage.ThrowMessage msg = new BYMessage.ThrowMessage
            {
                yootCount = yootCount
            };
            //turnSend.Client.myClient.Send(BYMessage.MyMsgType.ThrowResult, msg);
            BYClient.myClient.Send(BYMessage.MyMsgType.ThrowResult, msg);
        }
        else if (YootGame.isNetwork && owner.playerID == 1)
        {
            HandleThrowResult(yootCount);
        }
        if (currentState == ProcessState.Throw)
            HandleThrowResult(yootCount);
    }

    private void HandleThrowResult(YootGame.YootCount yootCount)
    {

        this.yootCount = yootCount;

        if (!YootGame.isNetwork)
        {
            if (yootCount == YootGame.YootCount.Nak)
                EndTurn();
            else
            {
                owner.horseManager.SetClickable(true);
                UpdateState(ProcessState.Horse);
            }
        }
        else
        {
            // TODO: "opponent yoot result: ~~" 메세지 나오게
            // TODO: 네트워크 상태에서의 상대 결과 처리
            if (owner.playerID == 1)
            {
                // 상대 말은 못건드리게
                if (yootCount == YootGame.YootCount.Nak)
                    EndTurn();
                else
                {

                    owner.horseManager.SetClickable(false);
                    UpdateState(ProcessState.Horse);
                }
            }
            else
            {
                if (yootCount == YootGame.YootCount.Nak)
                    EndTurn();
                else
                {
                    owner.horseManager.SetClickable(true);
                    UpdateState(ProcessState.Horse);
                }
            }
        }
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
        if (YootGame.isNetwork)
        {
            if (owner.playerID == 0)
            {
                Debug.Log("Send HorseSelect! horseID = " + horse.id);
                Debug.Log("Horse information: " + horse.ToString());
                BYMessage.HorseMessage msg = new BYMessage.HorseMessage
                {
                    horseID = horse.id
                };
                bool check = BYClient.myClient.Send(BYMessage.MyMsgType.SelectHorse, msg);
                Debug.Log("sending check = " + check);
            }
            else
            {
                Debug.Log("Recv HorseSelect!");
                lastPreview.button.onClick.RemoveAllListeners();
                Debug.Log("Remove button onClick listener!!");
            }
        }
        
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
        owner.horseManager.SetClickable(false);
        DestroyLastPreview();
        if(YootGame.isNetwork)
        {
            if (owner.playerID == 0)
            {
                Debug.Log("Send HorseAck!");
                BYMessage.HorseMessage msg = new BYMessage.HorseMessage
                {
                    horseID = -1
                };
                bool check = BYClient.myClient.Send(BYMessage.MyMsgType.SelectHorseAck, msg);
                Debug.Log("sending check = " + check);
            }
            else
            {
                TurnManager turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
                turnManager.ClearAll();

            }
        }
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
        if (YootGame.isNetwork && owner.playerID == 0)
        {
            Debug.Log("Send TurnEnd message!");
            BYClient.myClient.Send(BYMessage.MyMsgType.TurnEnd, turnSend.EmptyMsg);
        }
    }
}
