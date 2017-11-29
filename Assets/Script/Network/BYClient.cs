using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using MaterialUI;

public class BYClient : MonoBehaviour
{
    public NetworkClient myClient;
    int serverPort = 7000;
    string serverIP = "165.246.42.24";
    //string serverIP = "192.168.184.32";

    public delegate void OnNetworkActivity();

    bool isMatch = false;
    private BYMessage.EmptyMessage EmptyMsg;

    // Use this for initialization
    public void Start()
    {
        Debug.Log("BYClient Start() called");
        if (GameObject.Find("ClientManager"))
            return;
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
        myClient = new NetworkClient();
        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        Debug.Log("ConnectToServer() : " + myClient);
        // 이미 서버에 연결되어 있는 상태면 아무것도 안함
        if (isMatch)
            return;
        myClient.Connect(serverIP, serverPort);
        RegisterHandlers();
    }
    public void Cancel()
    {
        isMatch = false;
        Debug.Log("Cancel()" + myClient);
        Debug.Log("Match canceled");

        if(myClient.isConnected)
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
        SceneLoad a = GameObject.Find("SceneManager").GetComponent<SceneLoad>();
        a.LoadScene("Yoot");
    }
    
    // BUG 얘는 강제종료될때 호출이 안되는것같은데 잘 모르겠음
    private void OnApplicationQuit()
    {
        if (myClient.isConnected)
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