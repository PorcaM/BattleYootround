using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

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

    void Start()
    {
        Init();
        StartGame();
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
