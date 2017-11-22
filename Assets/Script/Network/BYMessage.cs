using System.Collections;
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
    public class MyMsgType
    {
        public static short MatchCancel = MsgType.Highest + 1;
        public static short MatchSuccess = MsgType.Highest + 2;

        public static short TurnStart = MsgType.Highest + 3;
        public static short TurnEnd = MsgType.Highest + 4;

        public static short YootScene = MsgType.Highest + 5;
        public static short MoveHorse = MsgType.Highest + 6;

        public static short BattleScene = MsgType.Highest + 7;
        public static short BattleWin = MsgType.Highest + 8;
        public static short BattleLose = MsgType.Highest + 9;


        public static short GameWin = MsgType.Highest + 10;
        public static short GameLose = MsgType.Highest + 11;

        public static short Disconnect = MsgType.Highest + 12;

    }
    
}
