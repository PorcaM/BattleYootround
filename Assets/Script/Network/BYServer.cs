using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYServer : NetworkManager
{
    public class MyMessage : MessageBase
    {
        public string str;
    }
    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
    }
    
    public NetworkDiscovery discovery;
    public GameObject ClientMsg;

    // MessageManager쪽 오버라이드
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer()");
    }
    public override void OnStartHost()
    {
        base.OnStartHost();
        Debug.Log("OnStartHost()");
    }

    // 서버 메세지 콜백
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
    public void OnMessage(NetworkMessage netMsg)
    {
        Debug.Log("On Message");

        MyMessage msg = netMsg.ReadMessage<MyMessage>();
        NetworkServer.SendToAll(MyMsgType.CustomMsgType, msg);

        ClientMsg.GetComponent<UnityEngine.UI.Text>().text = msg.str;
        Debug.Log("Message Received & Send Complete: " + msg.str);

        int len = NetworkServer.connections.Count;
        Debug.Log("connection Length: " + len);
        Debug.Log("IP :" + Network.player.ipAddress + ", ToString(): " + Network.player.ToString());
        

    }
    public void OnError(NetworkMessage netMsg)
    {
        
        var errorMsg = netMsg.ReadMessage<
        UnityEngine.Networking.NetworkSystem.ErrorMessage>();
        Debug.Log("Error:" + errorMsg.errorCode);
    }
    public void SetupServer()
    {
        Debug.Log("SetupServer()");
        StartServer();
        discovery.Initialize();
        discovery.StartAsServer();
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.Error, OnError);
        NetworkServer.RegisterHandler(MyMsgType.CustomMsgType, OnMessage);
    }
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

}
