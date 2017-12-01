using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkSendProcess : MonoBehaviour {
    public BYClient Client;
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;
    public ThrowProcessor yootThrowManager;

    private bool equip_state;

    public void Init()
    {
        //turnManager = GameObject.Find("TurnManager").GetComponent<YootGame>().turnManager;
        equip_state = false;

        Client = GameObject.Find("ClientManager").GetComponent<BYClient>();
        Debug.Log("TurnNetwork Send process init()... Client=" + Client);

        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        RegisterHandlers();
    }

    public void Ready()
    {
        StartCoroutine(WaitForEquip());
        SendEquipment();
    }
    public void SendEquipment()
    {
        Debug.Log("SendEquipment()");

        Equipment equip = GameObject.Find("Equipment").GetComponent<Equipment>();
        BYMessage.EquipmentMessage msg = new BYMessage.EquipmentMessage()
        {
            list = equip.ToIntArray()
        };
        Debug.Log(equip.ToString());
        BYClient.myClient.Send(BYMessage.MyMsgType.Equipment, msg);
    }

    IEnumerator WaitForEquip()
    {
        yield return new WaitWhile(() => equip_state == false);
        StartCoroutine(ReadyMessage());
    }
    IEnumerator ReadyMessage()
    {
        yield return new WaitForSeconds(2.0f);
        BYClient.myClient.Send(BYMessage.MyMsgType.YootReady, EmptyMsg);
    }
    private void RegisterHandlers()
    {
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.EquipmentReady, OnEquipmentReady);
    }

    private void OnEquipmentReady(NetworkMessage netMsg)
    {
        equip_state = true;
    }
    private void OnTurnStart(NetworkMessage netMsg)
    {
        Debug.Log("Turn Start Message Recieved!");
        turnManager.StartTurn(0);
    }
    
}
