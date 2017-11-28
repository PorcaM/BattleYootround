using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYGameManager : MonoBehaviour {
    private BYMessage.EmptyMessage EmptyMsg;

    private int player1;
    private int player2;
    private bool player1_yoot_ready;
    private bool player2_yoot_ready;
    private bool player1_battle_ready;
    private bool player2_battle_ready;
    
    private int startPlayer, nextPlayer;
    private bool isBattleOn;

    private void RegisterHandlers()
    {
        EmptyMsg = new BYMessage.EmptyMessage
        {
            str = ""
        };
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.YootReady, OnYootReady);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.ThrowForce, OnThrowForce);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.TurnEnd, OnTurnEnd);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.BattleReady, OnBattleReady);

        NetworkServer.RegisterHandler(BYMessage.MyMsgType.SelectHorse, OnSelectHorse);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.SelectHorseAck, OnSelectHorseAck);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.GameWin, OnGameWin);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.GameLose, OnGameLose);
    }

    public void GameInit(Pair<int, int> room)
    {

        player1 = room.First;
        player2 = room.Second;

        Debug.Log("SubThread - First: " + player1 + "  Second: " + player2);

        RegisterHandlers();

        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.MatchSuccess, EmptyMsg);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.MatchSuccess, EmptyMsg);

        player1_yoot_ready = false;
        player2_yoot_ready = false;
        player1_battle_ready = false;
        player2_battle_ready = false;
        isBattleOn = false;

        // player 둘 다 윷판 준비 될 때까지 대기
        StartCoroutine(WaitPlayersForYoot());
    }
    
    private void GameStart()
    {
        // 유저 턴 선택
        System.Random random = new System.Random();
        int turn = random.Next(1, 3);
        if (turn == 1)
        {
            startPlayer = player1;
            nextPlayer = player2;
        }
        else
        {
            startPlayer = player2;
            nextPlayer = player1;
        }
        Debug.Log(startPlayer + " player start!");
        BYMessage.PlayerInfo playerInfo = new BYMessage.PlayerInfo();
        playerInfo.PlayerNum = startPlayer;
        NetworkServer.SendToClient(startPlayer, BYMessage.MyMsgType.TurnStart, playerInfo);
        playerInfo.PlayerNum = nextPlayer;
        NetworkServer.SendToClient(nextPlayer, BYMessage.MyMsgType.TurnWait, playerInfo);
    }

    private void BattleStart()
    {
        Debug.Log("Battle Start!");

    }
    IEnumerator WaitPlayersForYoot()
    {
        Debug.Log("Wait players");
        yield return new WaitWhile(() => player1_yoot_ready == false || player2_yoot_ready == false);
        Debug.Log("players all ready");
        GameStart();
    }
    IEnumerator WaitPlayersForBattle()
    {
        Debug.Log("Wait players for Battle...");
        yield return new WaitWhile(() => player1_battle_ready == false || player2_battle_ready == false);
        Debug.Log("Battle is ready!");
        BattleStart();
    }

    // TODO(???)
    // 생각해보니 진짜로 멀티플레이면 매칭만 따로 잡고 여기서 NetworkServer선언 한번 더 해서 따로 연결해야 할듯 함
    // 안 그러면 Server로 전송하는 메세지도 겹칠테고 문제생길 것 같음.
    private void OnYootReady(NetworkMessage netMsg)
    {
        Debug.Log("Ready Message received!");
        int player = netMsg.conn.connectionId;
        if(player == player1)
        {
            Debug.Log(player1 + " is ready!");
            player1_yoot_ready = true;
        }
        else
        {
            Debug.Log(player2 + " is ready!");
            player2_yoot_ready = true;
        }
    }
    private void OnThrowForce(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        BYMessage.ThrowForceMessage msg = netMsg.ReadMessage<BYMessage.ThrowForceMessage>();
        Debug.Log("=====Torques======");
        for(int i=0; i<4; i++)
        {
            Debug.Log(msg.torques[i]);
        }
        Debug.Log("==================");
        if (player == player1)
        {
            Debug.Log(player1 + " throw force: " + msg.force);
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.ThrowForce, msg);
        }
        else
        {
            Debug.Log(player2 + " throw force: " + msg.force);
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.ThrowForce, msg);
        }
    }
    private void OnThrowResult(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        BYMessage.ThrowMessage msg = netMsg.ReadMessage<BYMessage.ThrowMessage>();
        YootGame.YootCount yootCount = msg.yootCount;
        if(player == player1)
        {
            Debug.Log(player1 + " throw result: " + yootCount);
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.ThrowResult, msg);
        }
        else
        {
            Debug.Log(player2 + " throw result: " + yootCount);
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.ThrowResult, msg);
        }
    }
    private void OnTurnEnd(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        if (player == player1)
        {
            Debug.Log(player1 + " player turn end");
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.TurnEnd, EmptyMsg);
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.TurnStart, EmptyMsg);
        }
        else
        {
            Debug.Log(player2 + " player turn end");
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.TurnStart, EmptyMsg);
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.TurnEnd, EmptyMsg);
        }
    }

    private void OnBattleReady(NetworkMessage netMsg)
    {
        Debug.Log("Ready Message received!");
        int player = netMsg.conn.connectionId;
        if (player == player1)
        {
            Debug.Log(player1 + " is ready!");
            player1_battle_ready = true;
        }
        else
        {
            Debug.Log(player2 + " is ready!");
            player2_battle_ready = true;
        }
    }
    private void OnSelectHorse(NetworkMessage netMsg)
    {
        BYMessage.HorseMessage msg = netMsg.ReadMessage<BYMessage.HorseMessage>();
        int player = netMsg.conn.connectionId;
        int opponent = (player == player1) ? player1 : player2;
        NetworkServer.SendToClient(opponent, BYMessage.MyMsgType.SelectHorse, msg);
    }
    private void OnSelectHorseAck(NetworkMessage netMsg)
    {
        BYMessage.HorseMessage msg = netMsg.ReadMessage<BYMessage.HorseMessage>();
        int player = netMsg.conn.connectionId;
        int opponent = (player == player1) ? player1 : player2;
        NetworkServer.SendToClient(opponent, BYMessage.MyMsgType.SelectHorseAck, msg);
    }
    private void OnGameWin(NetworkMessage netMsg)
    {
        int winner = netMsg.conn.connectionId;
        int loser = (winner == player1) ? player1 : player2;
        NetworkServer.SendToClient(winner, BYMessage.MyMsgType.GameWin, EmptyMsg);
        NetworkServer.SendToClient(loser, BYMessage.MyMsgType.GameLose, EmptyMsg);
    }
    private void OnGameLose(NetworkMessage netMsg)
    {
        int winner = netMsg.conn.connectionId;
        int loser = (winner == player1) ? player1 : player2;
        NetworkServer.SendToClient(winner, BYMessage.MyMsgType.GameWin, EmptyMsg);
        NetworkServer.SendToClient(loser, BYMessage.MyMsgType.GameLose, EmptyMsg);
    }
}
