using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYMessage : MonoBehaviour {

    public class MyMessage : MessageBase
    {
        public string str;
    }
    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
        public static short Yoot = MsgType.Highest + 2;
        public static short Battle = MsgType.Highest + 3;
        public static short Win = MsgType.Highest + 4;
        public static short Lose = MsgType.Highest + 5;
        public static short Disconnect = MsgType.Highest + 10;
    }

    public static void OnMessage(NetworkMessage netMsg)
    {
        BYMessage.MyMessage msg = netMsg.ReadMessage<BYMessage.MyMessage>();
        Debug.Log("Message Received: " + msg.str);
    }
    public static void OnError(NetworkMessage netMsg)
    {
        var errorMsg = netMsg.ReadMessage<UnityEngine.Networking.NetworkSystem.ErrorMessage>();
        Debug.Log("Error:" + errorMsg.errorCode);
    }
}
