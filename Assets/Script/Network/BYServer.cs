﻿using System.Collections;
using System.Collections.Generic;
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
    
    public NetworkDiscovery discovery;
    public GameObject ClientMsg;

    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "debug1";

    // 서버 메세지 콜백
    public void OnConnected(NetworkMessage netMsg)
    {
        debugMessage1 = "Connected to server";
        Debug.Log(debugMessage1);
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

        debugMessage1 = string.Format("IP :" + Network.player.ipAddress + ", ToString(): " + Network.player.ToString());
        Debug.Log(debugMessage1);

    }
    public void OnError(NetworkMessage netMsg)
    {
        
        var errorMsg = netMsg.ReadMessage<
        UnityEngine.Networking.NetworkSystem.ErrorMessage>();
        Debug.Log("Error:" + errorMsg.errorCode);
    }
    int minPort = 7000;
    int maxPort = 7000;
    int defaultPort = 7000;
    //string serverIP = "127.0.0.1";

    //Creates a server then returns the port the server is created with. Returns -1 if server is not created
    int createServer()
    {
        int serverPort = -1;
        //Connect to default port
        bool serverCreated = NetworkServer.Listen( defaultPort);
        if (serverCreated)
        {
            serverPort = defaultPort;
            Debug.Log("Server Created with deafault port");
        }
        else
        {
            Debug.Log("Failed to create with the default port");
            //Try to create server with other port from min to max except the default port which we trid already
            for (int tempPort = minPort; tempPort <= maxPort; tempPort++)
            {
                //Skip the default port since we have already tried it
                if (tempPort != defaultPort)
                {
                    //Exit loop if successfully create a server
                    if (NetworkServer.Listen( tempPort))
                    {
                        serverPort = tempPort;
                        break;
                    }

                    //If this is the max port and server is not still created, show, failed to create server error
                    if (tempPort == maxPort)
                    {
                        Debug.LogError("Failed to create server");
                    }
                }
            }
        }
        return serverPort;
    }

    // Use this for initialization
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
    // Update is called once per frame
    void Update()
    {
        UpdateDebug1Text(debugMessage1);
    }
    
}
