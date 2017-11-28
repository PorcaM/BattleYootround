﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkRecvProcess : MonoBehaviour {
    public BYClient Client;
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;
    public TurnProcessor turnProcessor;
    public ThrowProcessor yootThrowManager;

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
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowForce, OnThrowForce);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorse, OnSelectHorse);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorseAck, OnSelectHorseAck);
    }
    private void OnTurnWait(NetworkMessage netMsg)
    {
        Debug.Log("Turn Wait Message Recieved!");
        turnManager.StartTurn(1);
    }
    private void OnThrowForce(NetworkMessage netMsg)
    {
        Debug.Log("Force message Recieved!");
        BYMessage.ThrowForceMessage msg = netMsg.ReadMessage<BYMessage.ThrowForceMessage>();
        List<Vector3> torques = new List<Vector3>();
        // 전달받은 배열을 다시 List형태로 변환
        foreach(Vector3 torque in msg.torques)
        {
            torques.Add(torque);
        }
        Debug.Log("Force Received");
        Debug.Log("force: " + msg.force + ", torques: " + torques);
        yootThrowManager.createdModule.RecvMessage(msg.force, torques);
    }
    private void OnThrowResult(NetworkMessage netMsg)
    {
        BYMessage.ThrowMessage msg = netMsg.ReadMessage<BYMessage.ThrowMessage>();
        Debug.Log("Yoot Result message Recieved: " + msg.yootCount.ToString());
        turnProcessor.RecvThrowResult(msg.yootCount);
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
