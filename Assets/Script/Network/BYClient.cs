using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYClient : MonoBehaviour {
    NetworkClient myClient;
    int serverPort = 7000;
    string serverIP = "165.246.42.24";

    public delegate void OnNetworkActivity();
    public event OnNetworkActivity OnConnection;

    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "debug1";
    public GameObject IdField;
    public GameObject ServerMsg;


    public void SendMessage(short Type, BYMessage.MyMessage Msg)
    {
        Debug.Log(Msg.str);
        myClient.Send(Type, Msg);
    }

    public void OnMessage(NetworkMessage netMsg)
    {
        BYMessage.MyMessage msg = netMsg.ReadMessage<BYMessage.MyMessage>();
        ServerMsg.GetComponent<UnityEngine.UI.Text>().text = msg.str;
        Debug.Log("Message Received: " + msg.str);
    }
    private void OnConnect(NetworkMessage msg)
    {
        debugMessage1 = "Connected to server";
        Debug.Log(debugMessage1);
        if (OnConnection != null)
        {
            debugMessage1 = "OnConnection called";
            Debug.Log(debugMessage1);
            OnConnection();
        }
    }
    private void OnDisconnect(NetworkMessage netMsg)
    {
        // TODO: 상대방의 연결이 끊기면 서버에서 Disconnect를 주고 연결을 끊음
        //       클라이언트에선 이 메세지를 받으면 메세지를 띄우던가, 씬 전환을 해야 함.
    }

    // Use this for initialization
    void Start()
    {
        myClient = new NetworkClient();
    }

    public void ConnectToServer()
    {
        debugMessage1 = "Connect To Server() called";
        Debug.Log(debugMessage1);
        myClient.RegisterHandler(MsgType.Connect, OnConnect);
        myClient.Connect(serverIP, serverPort);
        myClient.RegisterHandler(BYMessage.MyMsgType.CustomMsgType, OnMessage); // TODO: BYMessage.OnMessage로 변경
        myClient.RegisterHandler(BYMessage.MyMsgType.Disconnect, OnDisconnect);
    }

    // 얘는 강제종료될때 호출이 안됨
    private void OnApplicationQuit()
    {
        BYMessage.MyMessage msg = new BYMessage.MyMessage();
        SendMessage(BYMessage.MyMsgType.Disconnect, msg);
    }
    /*
     * 
     *  나중에 안드로이드로 빌드할 때 Pause 걸리면 네트워크 끊어지도록 구현
     * 
     * 
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SendMessage(BYMessage.MyMsgType.Disconnect, new BYMessage.MyMessage());
        }
    }
    */
    private void UpdateDebug1Text(string message)
    {
        debugText1.text = message;
        debugText1.fontSize = 20;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateDebug1Text(debugMessage1);
    }
}
