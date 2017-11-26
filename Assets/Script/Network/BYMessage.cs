﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYMessage : MonoBehaviour {
    public class EmptyMessage : MessageBase
    {
        public string str;
    }
    public class HorseMessage : MessageBase
    {
        public int horseNum;
        public int destination;
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
        public static short YootReady = MsgType.Highest + 5;
        
        // Turn Message
        public static short TurnStart = MsgType.Highest + 10;
        public static short TurnEnd = MsgType.Highest + 11;
        public static short WaitTurn = MsgType.Highest + 12;

        public static short ThrowResult = MsgType.Highest + 20;
        public static short MoveHorse = MsgType.Highest + 30;


        // Battle Ready, and Win & Lost
        public static short BattleOccur = MsgType.Highest + 60;
        public static short BattleReady = MsgType.Highest + 70;
        public static short BattleWin = MsgType.Highest + 80;
        public static short BattleLose = MsgType.Highest + 90;
        
        // Game Win & Lose
        public static short GameWin = MsgType.Highest + 100;
        public static short GameLose = MsgType.Highest + 110;

        // Disconnect
        public static short Disconnect = MsgType.Highest + 150;
    }
    
}
