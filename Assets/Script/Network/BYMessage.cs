﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYMessage : MonoBehaviour {
    public class EmptyMessage : MessageBase
    {
        public string str;
    }

    public class EquipmentMessage : MessageBase
    {
        public int[] list = new int[9];
    }
    // 말 선택 & 움직임
    public class HorseMessage : MessageBase
    {
        public int horseID;
    }
    // 윷 던졌을 때 힘/토크
    public class ThrowForceMessage : MessageBase
    {
        public float force;
        public Vector3[] torques = new Vector3[4];
    }
    // 윷 던진 후 결과
    public class ThrowMessage : MessageBase
    {
        public YootGame.YootCount yootCount;
    }
    public class PlayerInfo : MessageBase
    {
        public int PlayerNum;
    }
    public class MyMsgType
    {
        // Match cancel by client, Match success by server
        public static short MatchCancel = MsgType.Highest + 1;
        public static short MatchSuccess = MsgType.Highest + 2;
        
        // Yoot ready before start turn
        public static short Equipment = MsgType.Highest + 5;
        public static short EquipmentReady = MsgType.Highest + 6;
        public static short YootReady = MsgType.Highest + 8;
        
        // Turn Message
        public static short TurnStart = MsgType.Highest + 10;
        public static short TurnEnd = MsgType.Highest + 11;
        public static short TurnWait = MsgType.Highest + 12;

        public static short ThrowForce = MsgType.Highest + 15;
        public static short ThrowResult = MsgType.Highest + 20;
        public static short SelectHorse = MsgType.Highest + 30;
        public static short SelectHorseAck = MsgType.Highest + 40;


        // Battle Ready, start, spell, and Win & Lose
        public static short BattleOccur = MsgType.Highest + 50;
        public static short BattleOccurReady = MsgType.Highest + 55;
        public static short BattleReady = MsgType.Highest + 60;
        public static short BattleStart = MsgType.Highest + 70;

        public static short SpellUse = MsgType.Highest + 75;

        public static short BattleWin = MsgType.Highest + 80;
        public static short BattleLose = MsgType.Highest + 90;
        
        // Game Win & Lose
        public static short GameWin = MsgType.Highest + 100;
        public static short GameLose = MsgType.Highest + 110;

        // Disconnect
        public static short Disconnect = MsgType.Highest + 150;
    }
    
}
