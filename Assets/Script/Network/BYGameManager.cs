using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYGameManager : MonoBehaviour {
    private BYMessage.EmptyMessage EmptyMsg;

    private int player1;
    private int player2;

    // equip -> ready -> gamestart (yoot & battle)
    private bool player1_equip_ready;
    private bool player2_equip_ready;
    private bool player1_yoot_ready;
    private bool player2_yoot_ready;
    private bool player1_unit_info;
    private bool player2_unit_info;
    BYMessage.UnitPositionMessage player1_unit = new BYMessage.UnitPositionMessage();
    BYMessage.UnitPositionMessage player2_unit = new BYMessage.UnitPositionMessage();
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
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.Equipment, OnEquipment);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.YootReady, OnYootReady);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.ThrowForce, OnThrowForce);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.ThrowResult, OnThrowResult);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.TurnEnd, OnTurnEnd);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.SelectHorse, OnSelectHorse);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.SelectHorseAck, OnSelectHorseAck);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.BattleOccur, OnBattleOccur);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.UnitPosition, OnUnitPosition);
        NetworkServer.RegisterHandler(BYMessage.MyMsgType.BattleReady, OnBattleReady);

        NetworkServer.RegisterHandler(BYMessage.MyMsgType.SpellUse, OnSpellUse);

        NetworkServer.RegisterHandler(BYMessage.MyMsgType.BattleWin, OnBattleWin);

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

        player1_equip_ready = false;
        player2_equip_ready = false;
        player1_yoot_ready = false;
        player2_yoot_ready = false;
        player1_battle_ready = false;
        player2_battle_ready = false;
        isBattleOn = false;

        // player들의 Equipment준비될 때까지 대기
        StartCoroutine(WaitPlayersForEquip());
    }

    IEnumerator WaitPlayersForEquip()
    {
        Debug.Log("Wait player equip");
        yield return new WaitWhile(() => player1_equip_ready == false || player2_equip_ready == false);
        Debug.Log("all player equip ready");
        BYServer.debugMessage1 = "all player equip ready";
        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.EquipmentReady, EmptyMsg);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.EquipmentReady, EmptyMsg);

        AfterEquipExchange();
    }

    private void AfterEquipExchange()
    {
        StartCoroutine(WaitPlayersForYoot());
    }

    IEnumerator WaitPlayersForYoot()
    {
        Debug.Log("Wait players");
        yield return new WaitWhile(() => player1_yoot_ready == false || player2_yoot_ready == false);
        Debug.Log("players all ready");
        BYServer.debugMessage1 = "players all ready!";
        GameStart();
    }

    private void GameStart()
    {
        // 유저 턴 선택
        int turn = Random.Range(1, 2);
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
        StartCoroutine(StartMessage());
    }
    private void GameContinue(int start, int next)
    {
        startPlayer = start;
        nextPlayer = next;
        StartCoroutine(StartMessage());
    }

    IEnumerator StartMessage()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log(startPlayer + " player start!");
        BYServer.debugMessage1 = string.Format("{0} player start!", startPlayer);
        BYMessage.PlayerInfo playerInfo = new BYMessage.PlayerInfo();
        playerInfo.PlayerNum = startPlayer;
        NetworkServer.SendToClient(startPlayer, BYMessage.MyMsgType.TurnStart, playerInfo);
        playerInfo.PlayerNum = nextPlayer;
        NetworkServer.SendToClient(nextPlayer, BYMessage.MyMsgType.TurnWait, playerInfo);
    }
    
    private void OnEquipment(NetworkMessage netMsg)
    {
        BYMessage.EquipmentMessage msg = netMsg.ReadMessage<BYMessage.EquipmentMessage>();

        BYServer.debugMessage1 = "OnEquipment()";
        
        int player = netMsg.conn.connectionId;
        if (player == player1)
        {
            NetworkServer.SendToClient(player2, BYMessage.MyMsgType.Equipment, msg);
            player2_equip_ready = true;
            BYServer.debugMessage1 = "player2 equip ready";
        }
        else
        {
            NetworkServer.SendToClient(player1, BYMessage.MyMsgType.Equipment, msg);
            player1_equip_ready = true;
            BYServer.debugMessage1 = "player1 equip ready";
        }
    }
    private void OnYootReady(NetworkMessage netMsg)
    {
        Debug.Log("Ready Message received!");
        int player = netMsg.conn.connectionId;
        if(player == player1)
        {
            Debug.Log(player1 + " is ready!");
            BYServer.debugMessage1 = string.Format("{0} player ready", player1);
            player1_yoot_ready = true;
        }
        else
        {
            Debug.Log(player2 + " is ready!");
            BYServer.debugMessage1 = string.Format("{0} player ready", player2);
            player2_yoot_ready = true;
        }
    }
    private void OnThrowForce(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        BYMessage.ThrowForceMessage msg = netMsg.ReadMessage<BYMessage.ThrowForceMessage>();
        /*
        Debug.Log("=====Torques======");
        for(int i=0; i<4; i++)
        {
            Debug.Log(msg.torques[i]);
        }
        Debug.Log("==================");
        */
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

    /************************************************
     * 
     *                  Battle
     *          
     ************************************************/
    private void OnBattleOccur(NetworkMessage netMsg)
    {
        // Battle occur 메세지를 준 클라이언트가 현재 턴의 플레이어
        // TODO: 배틀 승패에 따라 startPlayer, nextPlayer swap 후 GameStart함수 호출
        startPlayer = netMsg.ReadMessage<BYMessage.PlayerInfo>().PlayerNum;
        nextPlayer = (startPlayer == player2) ? player1 : player2;

        BYServer.debugMessage1 = string.Format("{0} player turn Battle occured!!", startPlayer);
        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.GiveMeUnitInfo, EmptyMsg);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.GiveMeUnitInfo, EmptyMsg);

        StartCoroutine(WaitPlayersForUnitInfo());
    }

    IEnumerator WaitPlayersForUnitInfo()
    {
        Debug.Log("Wait players for unit info");
        BYServer.debugMessage1 = "Wait players for unit info";
        yield return new WaitWhile(() => player1_unit_info == false || player2_unit_info == false);
        Debug.Log("All players unit info received!");
        BYServer.debugMessage1 = "All players unit info received!";

        GetRandomUnitPosition();
    }

    private void OnUnitPosition(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        if(player == player1)
        {
            player1_unit = netMsg.ReadMessage<BYMessage.UnitPositionMessage>();
            player1_unit_info = true;
        }
        else
        {
            player2_unit = netMsg.ReadMessage<BYMessage.UnitPositionMessage>();
            player2_unit_info = true;
        }
    }
    private void GetRandomUnitPosition()
    {
        UnitInstanceFactory getPos = new UnitInstanceFactory();
        Vector3[] player1_pos = new Vector3[15];
        Vector3[] player2_pos = new Vector3[15];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                player1_unit.ally_pos[i * 3 + j] = getPos.GetPosition(j, player1_unit.row[i], -1);
                player2_unit.enemy_pos[i * 3 + j] = getPos.GetPosition(j, player1_unit.row[i], 1);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                player1_unit.enemy_pos[i * 3 + j] = getPos.GetPosition(j, player1_unit.row[i], 1);
                player2_unit.ally_pos[i * 3 + j] = getPos.GetPosition(j, player1_unit.row[i], -1);
            }
        }
        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.ResultUnitPosition, player1_unit);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.ResultUnitPosition, player2_unit);
        StartCoroutine(WaitPlayersForBattle());
    }
    IEnumerator WaitPlayersForBattle()
    {
        Debug.Log("Wait players for Battle...");
        yield return new WaitWhile(() => player1_battle_ready == false || player2_battle_ready == false);
        Debug.Log("Battle is ready!");
        BattleStart();
    }

    private void BattleStart()
    {
        Debug.Log("Battle Start!");
        BYServer.debugMessage1 = "Battle Start!!";

        NetworkServer.SendToClient(player1, BYMessage.MyMsgType.BattleOccurReady, EmptyMsg);
        NetworkServer.SendToClient(player2, BYMessage.MyMsgType.BattleOccurReady, EmptyMsg);
        // TODO: Battle Unit들 hp 싱크 맞출수 있는 방법 찾아보기 ( [SyncVars] 비슷한거 본 것 같음 )
        // TODO: 배틀 사이사이 스펠 사용하는 것 Receive register하고 Send

        
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

    private void OnSpellUse(NetworkMessage netMsg)
    {
        int player = netMsg.conn.connectionId;
        int opponent = (player == player2) ? player1 : player2;
        BYMessage.SpellMessage msg = netMsg.ReadMessage<BYMessage.SpellMessage>();

        Debug.Log(player + "player using spell");
        Debug.Log(msg.pos);
        Debug.Log(msg.spellID);
        BYServer.debugMessage1 = string.Format("{0} player using spell. pos: {1}, id: {2}", player, msg.pos, msg.spellID);

        NetworkServer.SendToClient(opponent, BYMessage.MyMsgType.SpellUse, msg);
    }

    private void OnBattleWin(NetworkMessage netMsg)
    {
        int winner = netMsg.conn.connectionId;
        int loser = (winner == player2) ? player1 : player2;
        NetworkServer.SendToClient(winner, BYMessage.MyMsgType.BattleWin, EmptyMsg);
        NetworkServer.SendToClient(loser, BYMessage.MyMsgType.BattleLose, EmptyMsg);
        GameContinue(winner, loser);
    }

    /************************************************
     * 
     *                      Horse
     *                      
     ************************************************/
    private void OnSelectHorse(NetworkMessage netMsg)
    {
        BYMessage.HorseMessage msg = netMsg.ReadMessage<BYMessage.HorseMessage>();
        int player = netMsg.conn.connectionId;
        int opponent = (player == player1) ? player2 : player1;
        Debug.Log("Select horse message receive... and send. horseID = " + msg.horseID);
        NetworkServer.SendToClient(opponent, BYMessage.MyMsgType.SelectHorse, msg);
        Debug.Log("select horse message Successfully sended!!");
    }
    private void OnSelectHorseAck(NetworkMessage netMsg)
    {
        BYMessage.HorseMessage msg = netMsg.ReadMessage<BYMessage.HorseMessage>();
        int player = netMsg.conn.connectionId;
        int opponent = (player == player1) ? player2 : player1;
        Debug.Log("Select horse ack message receive... and send now");
        NetworkServer.SendToClient(opponent, BYMessage.MyMsgType.SelectHorseAck, msg);
        Debug.Log("select horse ack message Successfully sended!!");
    }

    /***************************************************
     * 
     *                  Result
     * 
     * *************************************************/
    private void OnGameWin(NetworkMessage netMsg)
    {
        int winner = netMsg.conn.connectionId;
        int loser = (winner == player1) ? player2 : player1;
        NetworkServer.SendToClient(winner, BYMessage.MyMsgType.GameWin, EmptyMsg);
        NetworkServer.SendToClient(loser, BYMessage.MyMsgType.GameLose, EmptyMsg);
    }
    private void OnGameLose(NetworkMessage netMsg)
    {
        int winner = netMsg.conn.connectionId;
        int loser = (winner == player1) ? player2 : player1;
        NetworkServer.SendToClient(winner, BYMessage.MyMsgType.GameWin, EmptyMsg);
        NetworkServer.SendToClient(loser, BYMessage.MyMsgType.GameLose, EmptyMsg);
    }
}
