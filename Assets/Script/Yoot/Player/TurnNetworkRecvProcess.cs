using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkRecvProcess : MonoBehaviour {
    public BYClient Client;
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;

    public void Init()
    {
        //turnManager = GameObject.Find("TurnManager").GetComponent<YootGame>().turnManager;

        Client = GameObject.Find("ClientManager").GetComponent<BYClient>();
        Debug.Log("TurnNetwork Recv process init()... Client=" + Client);

        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        RegisterHandlers();
    }
    private void RegisterHandlers()
    {
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.TurnWait, OnTurnWait);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorse, OnSelectHorse);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorseAck, OnSelectHorseAck);
    }
    
    private void OnTurnWait(NetworkMessage netMsg)
    {
        Debug.Log("Turn Wait Message Recieved!");
        turnManager.StartTurn(1);
    }
    private void OnThrowResult(NetworkMessage netMsg)
    {
        // TODO: 상대방의 윷 결과 show
    }
    private void OnSelectHorse(NetworkMessage netMsg)
    {
        // TODO: 상대방의 말 선택 효과
    }
    private void OnSelectHorseAck(NetworkMessage netMsg)
    {
        // TODO: 상대방의 말 움직임 효과
    }

}
