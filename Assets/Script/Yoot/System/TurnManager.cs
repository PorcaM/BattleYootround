using System.Collections;
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
        gameStateUI.SetColor(player);
        playerManager.GetPlayer(player).turnProcessor.StartTurn();
    }
}
