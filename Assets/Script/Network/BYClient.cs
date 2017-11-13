using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYClient : NetworkManager {
    public class MyMessage : MessageBase
    {
        public string str;
    }
    public class MyMsgType
    {
        public static short CustomMsgType = MsgType.Highest + 1;
    }

    NetworkClient myClient;
    public NetworkDiscovery discovery;
    public GameObject IdField;
    public GameObject ServerMsg;

    // MessageManager쪽 오버라이드
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("OnStartClient()");
    }
    public override void OnStopClient()
    {
        Debug.Log("OnStopClient()");
    }


    // 클라이언트
    public void SetupClient()
    {
        Debug.Log("SetupClient");
        StartClient();
        discovery.Initialize();
        discovery.StartAsClient();

        myClient = new NetworkClient();
        myClient.Connect("127.0.0.1", 4444);
        myClient.RegisterHandler(MyMsgType.CustomMsgType, OnMessage);
    }
    public new void SendMessage(string str)
    {
        MyMessage msg = new MyMessage();
        msg.str = str;
        //Debug.Log(msg.str);
        msg.str = IdField.GetComponent<UnityEngine.UI.Text>().text;
        //Debug.Log(msg.str);

        myClient.Send(MyMsgType.CustomMsgType, msg);

    }
    public void OnMessage(NetworkMessage netMsg)
    {
        MyMessage msg = netMsg.ReadMessage<MyMessage>();
        ServerMsg.GetComponent<UnityEngine.UI.Text>().text = msg.str;
        Debug.Log("Message Received: " + msg.str);
    }
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
