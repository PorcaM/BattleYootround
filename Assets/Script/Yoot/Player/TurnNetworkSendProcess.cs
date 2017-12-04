using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkSendProcess : MonoBehaviour {
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;
    public ThrowProcessor yootThrowManager;
    public Equipment equipment;

    private bool equip_state;

    public void Init()
    {
        equip_state = false;

        equipment = GameObject.Find("Equipment").GetComponent<Equipment>();

        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        RegisterHandlers();
    }
    private void RegisterHandlers()
    {
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.EquipmentReady, OnEquipmentReady);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.GiveMeUnitInfo, OnGiveMeUnitInfo);
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
        yield return new WaitForSeconds(1.0f);
        BYClient.myClient.Send(BYMessage.MyMsgType.YootReady, EmptyMsg);
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
    private void OnGiveMeUnitInfo(NetworkMessage netMsg)
    {
        BYMessage.UnitPositionMessage msg = new BYMessage.UnitPositionMessage();
        int row_i = 0;
        foreach (Unit unit in equipment.deck.Units)
            msg.row[row_i] = unit.position;
        BYClient.myClient.Send(BYMessage.MyMsgType.UnitPosition, msg);
    }
}
