using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using MaterialUI;

public class BYClient : MonoBehaviour
{
    public static NetworkClient myClient;
    int serverPort = 7000;
    //string serverIP = "165.246.42.24";
    string serverIP = "192.168.184.32";

    public delegate void OnNetworkActivity();

    bool isMatch = false;
    private BYMessage.EmptyMessage EmptyMsg;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        myClient = new NetworkClient();
        EmptyMsg = new BYMessage.EmptyMessage();
        EmptyMsg.str = "";
        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        // 기본 제공
        myClient.RegisterHandler(MsgType.Connect, OnConnect);
        // 커스텀 타입
        myClient.RegisterHandler(BYMessage.MyMsgType.MatchCancel, OnCancel);
        myClient.RegisterHandler(BYMessage.MyMsgType.MatchSuccess, OnMatchSuccess);
        myClient.RegisterHandler(BYMessage.MyMsgType.MoveHorse, OnMoveHorse);
        myClient.RegisterHandler(BYMessage.MyMsgType.BattleLose, OnBattleLose);
        myClient.RegisterHandler(BYMessage.MyMsgType.GameLose, OnGameLose);
    }
    public void ConnectToServer()
    {
        // 이미 서버에 연결되어 있는 상태면 아무것도 안함
        if (isMatch)
            return;
        myClient.Connect(serverIP, serverPort);
    }
    public void Cancel()
    {
        isMatch = false;
        myClient.Send(BYMessage.MyMsgType.MatchCancel, EmptyMsg);
    }

    // 서버에 연결됨 (Matching이 정상적으로 등록)
    private void OnConnect(NetworkMessage msg)
    {
        isMatch = true;
        Debug.Log("Connected to server");
    }
    private void OnCancel(NetworkMessage netMsg)
    {
        ToastManager.Show("Match Canceled");
    }
    private void OnMatchSuccess(NetworkMessage netMsg)
    {
        ToastManager.Show("Match Success!");
        // TODO: sleep 추가
        SceneLoad a = new SceneLoad();
        a.LoadScene("Yoot");
    }
    // 상대한테 받는거
    private void OnMoveHorse(NetworkMessage netMsg)
    {
        BYMessage.HorseMessage horseInfo = netMsg.ReadMessage<BYMessage.HorseMessage>();
        // 말 옮기기

    }
    private void OnBattleLose(NetworkMessage netMsg)
    {
        // 배틀 패배
    }
    private void OnGameLose(NetworkMessage netMsg)
    {
        // 게임 패배
    }
    // BUG 얘는 강제종료될때 호출이 안되는것같은데 잘 모르겠음
    private void OnApplicationQuit()
    {
        myClient.Send(BYMessage.MyMsgType.Disconnect, EmptyMsg);
    }
    /*
     *  나중에 안드로이드로 빌드할 때 Pause 걸리면 네트워크 끊어지도록 구현??
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SendMessage(BYMessage.MyMsgType.Disconnect, new BYMessage.MyMessage());
        }
    }
    */


}