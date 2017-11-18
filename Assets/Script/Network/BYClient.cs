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

    public class MyMessage : MessageBase
    {
        public string str;
    }
    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
    }

    public void sendMessage()
    {
        MyMessage msg = new MyMessage();
        
        msg.str = IdField.GetComponent<UnityEngine.UI.Text>().text;
        Debug.Log(msg.str);

        myClient.Send(MyMsgType.CustomMsgType, msg);

    }
    public void OnMessage(NetworkMessage netMsg)
    {
        MyMessage msg = netMsg.ReadMessage<MyMessage>();
        ServerMsg.GetComponent<UnityEngine.UI.Text>().text = msg.str;
        Debug.Log("Message Received: " + msg.str);
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
        myClient.RegisterHandler(MyMsgType.CustomMsgType, OnMessage);
    }
    public void OnConnect(NetworkMessage msg)
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
