using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class BYServer : MonoBehaviour
{
    
    public GameObject ClientMsg;
    public GameObject roomPrefab;

    //public GameObject cMsg1;
    //public GameObject cMsg2;

    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "debugMessage";

    int NumClient = 0;
    int NumMatch = 0;
    bool isMatch = false;
    int defaultPort = 7000;

    List<Pair<int, int>> roomList = new List<Pair<int,int>>();
    Pair<int, int> room;

    private BYMessage.EmptyMessage EmptyMsg;
    
    public void checkClient()
    {
        foreach(NetworkConnection nc in NetworkServer.connections)
        {
            Debug.Log(nc.connectionId);
        }
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
        EmptyMsg = new BYMessage.EmptyMessage();
        EmptyMsg.str = "";

        RegisterHandlers();
    }
    
    private void RegisterHandlers()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.MatchCancel, OnCancel);

    }
    // 클라이언트와 연결되었을 때 호출됨
    // TODO: 클라이언트 접속종료 때 호출되는 함수 필요
    // TODO: 기존 클라이언트 접속종료 시, NetworkServer.connections의 indexing이 변함 이거 처리해야함
    private void OnConnected(NetworkMessage netMsg)
    {

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
            Debug.Log("MainThread - First: " + room.First + "  Second: " + room.Second);
            roomList.Add(room);

            /*
            Thread t = new Thread(delegate ()
            {
                BYGameManager BYgm = new BYGameManager();
                BYgm.GameStart(room);
            });
            t.Start();
            */
            roomPrefab = Instantiate(roomPrefab);
            roomPrefab.GetComponent<BYGameManager>().GameInit(room);

            NumMatch++;
        }
        else
        {
            isMatch = true;
        }
    }
    private void DeletePlayerInRoomList(int disconnected_client)
    {
        isMatch = false;
        NetworkServer.connections[disconnected_client].Disconnect();
        int opponent = GetOpponent(disconnected_client);
        if (opponent != -1)
        {
            NetworkServer.connections[opponent].Disconnect();
            Debug.Log(disconnected_client + "," + opponent + " successfully disconnected.");
            NumMatch--;
        }
    }

    // roomList 순회하면서 해당 유저의 상대를 검색
    private int GetOpponent(int player1)
    {
        int player2 = -1;
        for (int i = roomList.Count - 1; i >= 0; --i)
        {
            if (roomList[i].First == player1)
            {
                player2 = roomList[i].Second;
                break;
            }
            else if (roomList[i].Second == player1)
            {
                player2 = roomList[i].First;
                break;
            }
        }
        return player2;
    }

    // Matching Cancel
    private void OnCancel(NetworkMessage netMsg)
    {
        Debug.Log(netMsg.conn);
        DeletePlayerInRoomList(netMsg.conn.connectionId);
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
