using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class BYServer : MonoBehaviour
{
    public class MyMessage : MessageBase
    {
        public string str;
    }
    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
    }
    
    public GameObject ClientMsg;

    public GameObject cMsg1;
    public GameObject cMsg2;

    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "debugMessage";

    int NumClient = 0;
    int NumMatch = 0;
    bool isMatch = false;
    int defaultPort = 7000;

    List<Pair<int, int>> roomList = new List<Pair<int,int>>();
    Pair<int, int> room;

    // 클라이언트와 연결되었을 때 호출됨
    // TODO: 클라이언트 접속종료 때 호출되는 함수 필요
    // TODO: 기존 클라이언트 접속종료 시, connectionID는 그대로지만, NetworkServer.connections의 indexing이 변함 이거 처리해야함
    public void OnConnected(NetworkMessage netMsg)
    {
        //NumClient++;
        NumClient = NetworkServer.connections.Count - 1;
        debugMessage1 = string.Format("Connected to client #" + NumClient);
        Debug.Log(debugMessage1);
        
        if (isMatch)
        {
            isMatch = false;
            Debug.Log("There is two player. game start");
            int player1 = NetworkServer.connections[NumClient - 1].connectionId;
            int player2 = NetworkServer.connections[NumClient].connectionId;
            room = new Pair<int, int>(player1, player2);
            Debug.Log("First: " + room.First + "  Second: " + room.Second);
            Debug.Log(roomList.Count);
            roomList.Add(room);
            Debug.Log(roomList.Count);
            Thread t = new Thread(delegate ()
            {
                BYGame.OnGame(room);
            });
            t.Start();
            
            NumMatch++;
        }
        else
        {
            isMatch = true;
        }
        
        /*
        foreach (NetworkConnection nc in  NetworkServer.connections)
        {
            if (nc == null)
                continue;

            Debug.Log(nc.address);
            Debug.Log(nc.connectionId);
        }
        Debug.Log("------------------------------------------");
        */
    }

    

    public void OnMessage(NetworkMessage netMsg)
    {
        MyMessage msg = netMsg.ReadMessage<MyMessage>();

        ClientMsg.GetComponent<UnityEngine.UI.Text>().text = string.Format("Client: " + msg.str);

        int len = NetworkServer.connections.Count;
        Debug.Log("connection Length: " + len);

        debugMessage1 = string.Format("IP :" + Network.player.ipAddress + ", ToString(): " + Network.player.ToString());
        Debug.Log(debugMessage1);

    }
    public void OnError(NetworkMessage netMsg)
    {
        var errorMsg = netMsg.ReadMessage<UnityEngine.Networking.NetworkSystem.ErrorMessage>();
        Debug.Log("Error:" + errorMsg.errorCode);
    }

    public static void SendClient(int connectionId, string str)
    {
        MyMessage Msg = new MyMessage();
        Msg.str = str;
        NetworkServer.SendToClient(connectionId, MyMsgType.CustomMsgType, Msg);
    }

    // 테스트용
    public static void SendClient(int connectionId)
    {
        MyMessage Msg = new MyMessage();
        Msg.str = string.Format(connectionId + " was send by server");
        NetworkServer.SendToClient(connectionId, MyMsgType.CustomMsgType, Msg);
    }
    public void SendClient1()
    {
        SendClient(1);
    }
    public void SendClient2()
    {
        SendClient(2);
    }

    // defaultPort로 서버 생성, 실패 시 -1 반환
    int createServer()
    {
        int serverPort = defaultPort;
        bool serverCreated = NetworkServer.Listen( defaultPort);

        if (!serverCreated)
        {
            Debug.Log("Server Create Failed");
            return -1;
        }
        
        return serverPort;
    }
    
    void Start()
    {
        int serverPort = createServer();
        if (serverPort != -1)
        {
            Debug.Log("Server created on port : " + serverPort);
        }
        else
        {
            Debug.Log("Failed to create Server");
        }
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.Error, OnError);
        NetworkServer.RegisterHandler(MyMsgType.CustomMsgType, OnMessage);
    }


    private void UpdateDebug1Text(string message)
    {
        debugText1.text = message;
        debugText1.fontSize = 20;
    }
    void Update()
    {
        UpdateDebug1Text(debugMessage1);
    }
    
}
