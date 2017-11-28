using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class YootGame : MonoBehaviour {
    public enum GameMode { Local, Network };
    public GameMode gameMode;
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };

    public YootInitializer yootInitializer;
    public BattleManager battleManager;
    public GameStateUI gameStateUI;
    public PlayerManager playerManager;
    public TurnManager turnManager;
    public HorseTranslator horseTranslator;

    public GameObject ClientManager;
    public static BYClient Client;
    public static BYMessage.EmptyMessage EmptyMsg;
    void Start()
    {
        Init();
        if(gameMode==GameMode.Local)
            StartGame();
    }

    public void Init()
    {
        yootInitializer.Init();
        battleManager.Init();
        playerManager.Init();
        turnManager.Init(this);

        if (gameMode == GameMode.Network)
        {
            EmptyMsg = new BYMessage.EmptyMessage
            {
                str = ""
            };
            RegisterHandlers();
        }
    }

    private void RegisterHandlers()
    {
        ClientManager = GameObject.Find("ClientManager");

        Client = ClientManager.GetComponent<BYClient>();
        bool check = Client.myClient.Send(BYMessage.MyMsgType.YootReady, EmptyMsg);
        Debug.Log(check);

        Client.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.TurnWait, OnTurnWait);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        Client.myClient.RegisterHandler(BYMessage.MyMsgType.MoveHorse, OnMoveHorse);
    }
    private void OnTurnStart(NetworkMessage netMsg)
    {
        Debug.Log("Turn Start Message Recieved!");
        turnManager.StartTurn(0);
    }
    private void OnTurnWait(NetworkMessage netMsg)
    {
        Debug.Log("Turn Wait Message Recieved!");
        turnManager.StartTurn(1);
    }
    private void OnThrowResult(NetworkMessage netMsg)
    {
        // TODO: 상대방의 윷 결과 show
    }
    private void OnMoveHorse(NetworkMessage netMsg)
    {
        // TODO: 상대방의 말 움직이도록
    }

    private int GetFirstPlayer()
    {
        int firstPlayer = 0;
        return firstPlayer;
    }

    private void StartGame()
    {
        int firstPlayer = GetFirstPlayer();
        turnManager.StartTurn(firstPlayer);
    }

    public void EndTurn(int lastPlayer)
    {
        if (gameMode == GameMode.Local)
            turnManager.StartNextTurn(lastPlayer);
        else
        {

        }
    }

    public void HandleBattleResult(int winPlayer)
    {
        string content = "Battle winner is Player " + winPlayer + "!!";
        ToastManager.Show(content);
        turnManager.StartTurn(winPlayer);
    }

    public void HandleWin(int winner)
    {
        Debug.Log("Player " + winner + " Win!!");
        SceneManager.LoadScene("Result");
    }
}
