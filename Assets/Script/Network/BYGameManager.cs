﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYGameManager : MonoBehaviour {
    private BYMessage.EmptyMessage EmptyMsg;

    private int player1;
    private int player2;
    private bool player1_ready;
    private bool player2_ready;
    
    public void GameStart(Pair<int, int> room)
    {
        EmptyMsg = new BYMessage.EmptyMessage();
        EmptyMsg.str = "";
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.YootReady, OnYootReady);

        player1 = room.First;
        player2 = room.Second;

        Debug.Log("SubThread - First: " + player1 + "  Second: " + player2);

        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.MatchSuccess, EmptyMsg);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.MatchSuccess, EmptyMsg);

        player1_ready = false;
        player2_ready = false;


        // player 둘 다 윷판 준비 될 때까지 대기
        while(true)
        {
            if (player1_ready == true && player2_ready == true)
                break;
        }

        // 유저 턴 선택
        System.Random random = new System.Random();
        int turn = random.Next(1, 3);
        if (turn == 1)
        {
            BYMessage.PlayerInfo PInfo = new BYMessage.PlayerInfo();
            PInfo.PlayerNum = player1;
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.TurnStart, PInfo);
            PInfo.PlayerNum = player2;
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.WaitTurn, PInfo);
            Debug.Log(player1 + " start!");
        }
        else
        {
            BYMessage.PlayerInfo PInfo = new BYMessage.PlayerInfo();
            PInfo.PlayerNum = player2;
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.TurnStart, PInfo);
            PInfo.PlayerNum = player1;
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.WaitTurn, PInfo);
            Debug.Log(player2 + " start!");
        }

    }
    

    // TODO(???)
    // 생각해보니 진짜로 멀티플레이면 매칭만 따로 잡고 여기서 NetworkServer선언 한번 더 해서 따로 연결해야 할듯 함
    // 안 그러면 Server로 전송하는 메세지도 겹칠테고 문제생길듯.
    private void OnYootReady(NetworkMessage netMsg)
    {
        Debug.Log("Ready Message received!");
        int player = netMsg.conn.connectionId;
        if(player == player1)
        {
            Debug.Log(player1 + " is ready!");
            player1_ready = true;
        }
        else if(player == player2)
        {
            Debug.Log(player2 + " is ready!");
            player2_ready = true;
        }
        else
        {
            Debug.Log(player + ", Who are you??");
        }
    }
}
