﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    public YootGame yootGame;
    public GameStateUI gameStateUI;

    private PlayerManager playerManager;

    public void Init(YootGame yootGame)
    {
        this.yootGame = yootGame;
        playerManager = yootGame.playerManager;
    }

    public void StartNextTurn(int lastPlayer)
    {
        int nextPlayer = playerManager.GetNextPlayer(lastPlayer);
        StartTurn(nextPlayer);
    }

    public void StartTurn(int player)
    {
        ClearAll();
        gameStateUI.SetColor(player);
        playerManager.GetPlayer(player).turnProcessor.StartTurn();
    }

    public void ClearAll()
    {
        foreach (YootPlayer player in playerManager.players)
        {
            Debug.Log("ClearAll player...");
            player.turnProcessor.UpdateState(TurnProcessor.ProcessState.Wait);
            if (player.playerID==1 && player.turnProcessor.yootThrowManager.createdModule != null)
            {
                Debug.Log("Remove created Module");
                player.turnProcessor.yootThrowManager.createdModule.End();
            }
        }
    }
}
