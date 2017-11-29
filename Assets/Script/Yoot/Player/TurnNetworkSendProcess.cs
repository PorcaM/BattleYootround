using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkSendProcess : MonoBehaviour {
    public BYClient Client;
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;
    public ThrowProcessor yootThrowManager;

    public void Init()
    {
        //turnManager = GameObject.Find("TurnManager").GetComponent<YootGame>().turnManager;

        Client = GameObject.Find("ClientManager").GetComponent<BYClient>();
        Debug.Log("TurnNetwork Send process init()... Client=" + Client);
        Debug.Log(BYClient.myClient);
        Debug.Log(BYClient.myClient.isConnected);

        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        RegisterHandlers();
    }

    public void SendEquipment()
    {
        Debug.Log("SendEquipment()");
        Debug.Log(BYClient.myClient.isConnected);
        Equipment equip = GameObject.Find("Equipment").GetComponent<Equipment>();
        BYMessage.EquipmentMessage msg = new BYMessage.EquipmentMessage()
        {
            list = equip.ToIntArray()
        };
        Debug.Log(equip.ToString());
        BYClient.myClient.Send(BYMessage.MyMsgType.Equipment, msg);
    }

    public void Ready()
    {
        SendEquipment();
        bool check = BYClient.myClient.Send(BYMessage.MyMsgType.YootReady, EmptyMsg);

    }

    private void RegisterHandlers()
    {

        //Client.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        //Client.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
    }

    private void OnTurnStart(NetworkMessage netMsg)
    {
        Debug.Log("Turn Start Message Recieved!");
        turnManager.StartTurn(0);
    }
    private void OnThrowResult(NetworkMessage netMsg)
    {
        // TODO: 상대방의 윷 결과 show
    }
}
