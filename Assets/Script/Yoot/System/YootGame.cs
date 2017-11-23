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
        StartGame();
    }

    public void Init()
    {
        InitEntireSystem();
        InitMyMembers();
    }

    private void InitEntireSystem()
    {
        HorseRoute.Init();
        yootBoard.Init();
        UnitHealthBar.Init();

        equipment.TempInit();
        Debug.Log(equipment.ToString());
    }

    private void InitMyMembers()
    {
        playerManager.Init();
        battleManager.Init();
        turnManager.Init(this);
    }

    private int GetFirstPlayer()
    {
        int firstPlayer = 0;
        if (gameMode == GameMode.Network)
        {
            // TODO Recv first player id;
        }
        return firstPlayer;
    }

    private void StartGame()
    {
        int firstPlayer = GetFirstPlayer();
        turnManager.StartTurn(firstPlayer);
    }

    public void EndTurn(int lastPlayer)
    {
        turnManager.StartNextTurn(lastPlayer);
    }

    public void HandleBattleResult(int winPlayer)
    {
        string content = "Battle winner is Player " + winPlayer + "!!";
        ToastManager.Show(content);
        turnManager.StartTurn(winPlayer);
    }
}
