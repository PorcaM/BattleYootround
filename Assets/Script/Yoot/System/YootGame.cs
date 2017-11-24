using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
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

    private BYMessage.EmptyMessage EmptyMsg;
    void Start()
    {
        Init();
        if(gameMode==GameMode.Local)
            StartGame();
        else
        {
            EmptyMsg = new BYMessage.EmptyMessage();
            EmptyMsg.str = "";
        }
    }

    public void Init()
    {
        yootInitializer.Init();
        battleManager.Init();
        playerManager.Init();
        turnManager.Init(this);
        
        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        // Yoot씬에서 매칭없이 시작할 경우, myClient에 아무것도없어서 에러남
        // 개발단계에서만 쓸 임시 코드
        //if (BYClient.myClient == null)
        //    BYClient.myClient = new NetworkClient();

        BYClient.myClient.Send(BYMessage.MyMsgType.YootReady, EmptyMsg);

        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.TurnStart, OnTurnStart);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.WaitTurn, OnWaitTurn);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        BYClient.myClient.RegisterHandler(BYMessage.MyMsgType.MoveHorse, OnMoveHorse);
    }
    private void OnTurnStart(NetworkMessage netMsg)
    {
        turnManager.StartTurn(0);
    }
    private void OnWaitTurn(NetworkMessage netMsg)
    {
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
}
