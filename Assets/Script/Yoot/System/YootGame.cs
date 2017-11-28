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
    public static bool isNetwork;
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };


    public YootInitializer yootInitializer;
    public BattleManager battleManager;
    public GameStateUI gameStateUI;
    public PlayerManager playerManager;
    public TurnManager turnManager;
    public HorseTranslator horseTranslator;
    
    public TurnNetworkSendProcess turnSend;
    
    void Start()
    {
        GameInfo gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        gameMode = gameInfo.gameMode;
        if (gameMode == GameMode.Network)
            isNetwork = true;
        else
            isNetwork = false;
        Init();
        if (!isNetwork)
        {
            Debug.Log("Game Start on local");
            StartGame();
        }
        else
        {
            Debug.Log("Game Start on network");
            turnSend = playerManager.GetPlayer(0).turnProcessor.turnSend;
            Debug.Log("YootGame, turnSend: " + turnSend);
            turnSend.Ready();
            //bool check = turnSend.Client.myClient.Send(BYMessage.MyMsgType.YootReady, turnSend.EmptyMsg);
            //Debug.Log("result check: " + check);
        }
    }

    public void Init()
    {
        yootInitializer.Init();
        battleManager.Init();
        playerManager.Init();
        turnManager.Init(this);
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
        if (!isNetwork)
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
