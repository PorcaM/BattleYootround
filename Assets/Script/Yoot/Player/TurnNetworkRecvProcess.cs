﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnNetworkRecvProcess : MonoBehaviour {
    public BYMessage.EmptyMessage EmptyMsg;
    public TurnManager turnManager;
    public TurnProcessor turnProcessor;
    public ThrowProcessor yootThrowManager;

    public BattleGame battle;
    public enum BattleState { Idle, Standby, OnBattle, Finished }

    public void Init()
    {
        battle = GameObject.Find("BattleGame").GetComponent<BattleGame>();

        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };

        RegisterHandlers();
    }
    private void RegisterHandlers()
    {
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.Equipment, OnEquipment);

        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnWait, OnTurnWait);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnEnd, OnTurnEnd);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowForce, OnThrowForce);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);

        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorse, OnSelectHorse);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.SelectHorseAck, OnSelectHorseAck);

        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.BattleOccurReady, OnBattleOccurReady);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.ResultUnitPosition, OnResultUnitPosition);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.BattleStart, OnBattleStart);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.SpellUse, OnSpellUse);
    }
    private void OnEquipment(NetworkMessage netMsg)
    {
        Debug.Log("OnEquipment()");
        BYMessage.EquipmentMessage msg = netMsg.ReadMessage<BYMessage.EquipmentMessage>();

        Equipment op_equip = GameObject.Find("Equipment").GetComponent<Equipment>();
        op_equip = Instantiate(op_equip, GameObject.Find("Data").transform);
        op_equip.name = "Opponent Equipment";
        string str = "";
        foreach (int a in msg.list)
        {
            str += string.Format("{0}, ",a);
        }
        Debug.Log(str);
        op_equip.Init(msg.list);
        Debug.Log(op_equip.ToString());

    }
    private void OnTurnWait(NetworkMessage netMsg)
    {
        Debug.Log("Turn Wait Message Recieved!");
        turnManager.StartTurn(1);
    }
    private void OnTurnEnd(NetworkMessage netMsg)
    {
        Debug.Log("Turn End Message Recieved!");
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
        BYMessage.HorseMessage msg = netMsg.ReadMessage<BYMessage.HorseMessage>();
        Debug.Log("Horse Select message Recieved: " + msg.horseID);
        Horse horse = turnProcessor.owner.horseManager.GetHorse(msg.horseID);
        Debug.Log("Horse information: " + horse.ToString());
        turnProcessor.RecvHorseSelect(horse);
    }
    private void OnSelectHorseAck(NetworkMessage netMsg)
    {
        Debug.Log("Horse Select message Recieved: ");
        turnProcessor.RecvAck();
    }

    private void OnResultUnitPosition(NetworkMessage netMsg)
    {
        BYMessage.UnitPositionMessage msg = netMsg.ReadMessage<BYMessage.UnitPositionMessage>();

        battle.Init();
        battle.StartGame(msg);

        UnitInstanceFactory AFactory = GameObject.Find("AFactory").GetComponent<UnitInstanceFactory>();
        UnitInstanceFactory EFactory = GameObject.Find("EFactory").GetComponent<UnitInstanceFactory>();
        AFactory.CreateUnits(msg.ally_pos);
        EFactory.CreateUnits(msg.enemy_pos);
        Debug.Log("Unit result position received...");

        BYClient.myClient.Send(BYMessage.MyMsgType.BattleReady, EmptyMsg);
    }
    private void OnBattleOccurReady(NetworkMessage netMsg)
    {
        Debug.Log("Server send <battle occur ready> message...");
        battle.combatManager.StartBattle();
        battle.state = BattleGame.State.OnBattle;
        Debug.Log("Ready battle function successfully called");
        
    }
    private void OnBattleStart(NetworkMessage netMsg)
    {

    }
    private void OnSpellUse(NetworkMessage netMsg)
    {
        BYMessage.SpellMessage msg = netMsg.ReadMessage<BYMessage.SpellMessage>();
        Debug.Log("Opponent spell use message received!!");
        Debug.Log(msg.pos);
        Debug.Log(msg.spellID);
        SpellManifestator.EnemySpell(msg.pos, msg.spellID);
    }
}
