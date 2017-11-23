using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

public class YootGame : MonoBehaviour {
    public enum GameMode { Local, Network };
    public GameMode gameMode;
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };
    public YootBoard yootBoard;
    public BattleManager battleManager;
    public Equipment equipment;

    public GameStateUI gameStateUI;
    public HorseTranslator horseTranslator;
    public PlayerManager playerManager;
    public TurnManager turnManager;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        HorseRoute.Init();
        yootBoard.Init();

        playerManager.Init();

        UnitHealthBar.Init();
        equipment.TempInit();
        Debug.Log(equipment.ToString());
        battleManager.Init();

        playerManager.players[0].GetComponent<YootPlayer>().turnManager.StartTurn();
    }

    public void ExchangeTurn(int lastPlayer)
    {
        if (gameMode == GameMode.Local)
        {
            int nextPlayer = (lastPlayer + 1) % 2;
            gameStateUI.UpdateColor(nextPlayer);
            playerManager.players[nextPlayer].GetComponent<YootPlayer>().turnManager.StartTurn();
        }
    }

    public void HandleBattleResult(int winnerID)
    {
        string content = "Battle winner is Player " + winnerID + "!!";
        ToastManager.Show(content);
        playerManager.players[winnerID].GetComponent<YootPlayer>().turnManager.StartTurn();
    }
}
