﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using MaterialUI;

public class BYClient : MonoBehaviour
{
    public static NetworkClient myClient;
    int serverPort = 7000;
    //string serverIP = "address";
    string serverIP = "127.0.0.1";

    static bool isMatch;
    private BYMessage.EmptyMessage EmptyMsg;

    // Use this for initialization
    public void Start()
    {
        Debug.Log("BYClient Start() called");
        isMatch = false;
        myClient = new NetworkClient();
        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        RegisterHandlers();
        DontDestroyOnLoad(this);
    }

    private void RegisterHandlers()
    {
        // 기본 제공
        myClient.RegisterHandler(MsgType.Connect, OnConnect);
        // 커스텀 타입
        myClient.RegisterHandler(BYMessage.MyMsgType.MatchCancel, OnCancel);
        myClient.RegisterHandler(BYMessage.MyMsgType.MatchSuccess, OnMatchSuccess);
    }
    public void ConnectToServer()
    {
        Debug.Log("ConnectToServer() : " + myClient);
        // 이미 서버에 연결되어 있는 상태면 아무것도 안함
        GameObject Matching = GameObject.Find("Canvas").transform.Find("Matching").gameObject;
        Matching.SetActive(true);
        
        UnityEngine.UI.Button cancel = GameObject.Find("Matching").transform.Find("Button - Raised").GetComponent<UnityEngine.UI.Button>();
        cancel.onClick.AddListener(Cancel);

        if (isMatch)
        {
            Debug.Log("Server already did matching!");
            return;
        }
        
        myClient.Connect(serverIP, serverPort);
    }
    public void Cancel()
    {
        isMatch = false;
        
        Debug.Log("Match canceled");
        GameObject Matching = GameObject.Find("Matching");
        Matching.SetActive(false);

        if (myClient.isConnected)
        {
            Debug.Log("Cancel message Sended");
            myClient.Send(BYMessage.MyMsgType.MatchCancel, EmptyMsg);
        }
    }

    // 서버에 연결됨 (Matching이 정상적으로 등록)
    private void OnConnect(NetworkMessage msg)
    {
        Debug.Log("Connected to server");
        isMatch = true;
    }
    private void OnCancel(NetworkMessage netMsg)
    {
        ToastManager.Show("Match Canceled");
        Debug.Log("Match Canceled");
    }
    private void OnMatchSuccess(NetworkMessage netMsg)
    {
        ToastManager.Show("Match Success!");
        Debug.Log("Match Success!");
        SceneLoad a = GameObject.Find("SceneManager").GetComponent<SceneLoad>();
        isMatch = false;
        a.LoadScene("Yoot");
    }
    
}