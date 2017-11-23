using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

public class YootGame : MonoBehaviour {
    public enum GameMode { Local, Network };
    public GameMode gameMode;
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };
    public TurnManager turnManager;
    public GameObject enemyHorse;
    public YootBoard yootBoard;
    public BattleManager battleManager;
    public Equipment equipment;

    public GameObject playerPref;
    public Transform playerParent;
    public List<GameObject> players;
    public const int playerCount = 2;

    public Text playerText;
    public int currentPlayer;

    public GameStateUI gameStateUI;

    public void SetCurrentPlayer(int playerID)
    {
        currentPlayer = playerID;
        playerText.text = "Turn of Player: " + currentPlayer;
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        HorseRoute.Init();
        yootBoard.Init();

        InitPlayers();

        UnitHealthBar.Init();
        equipment.TempInit();
        Debug.Log(equipment.ToString());
        battleManager.Init();

        players[0].GetComponent<YootPlayer>().turnManager.StartTurn();
    }

    private void CreatePlayers()
    {
        players = new List<GameObject>();
        for (int i = 0; i < playerCount; ++i)
        {
            GameObject playerObj = Instantiate(playerPref, playerParent);
            playerObj.GetComponent<YootPlayer>().Init();
            players.Add(playerObj);
        }
    }

    private void InitPlayers()
    {
        foreach(GameObject playerObj in players)
        {
            playerObj.GetComponent<YootPlayer>().Init();
        }
    }

    public void TestEnemyHorse()
    {
        GameObject horse = Instantiate(enemyHorse) as GameObject;
        YootBoard.fieldObjs[10].GetComponent<YootField>().Arrive(horse.GetComponent<Horse>());
    }

    public void ExchangeTurn(int lastPlayer)
    {
        if (gameMode == GameMode.Local)
        {
            int nextPlayer = (lastPlayer + 1) % 2;
            gameStateUI.UpdateColor(nextPlayer);
            players[nextPlayer].GetComponent<YootPlayer>().turnManager.StartTurn();
        }
    }

    public void HandleBattleResult(int winnerID)
    {
        string content = "Winner is Player " + winnerID + "!!";
        ToastManager.Show(content);
        players[winnerID].GetComponent<YootPlayer>().turnManager.StartTurn();
    }
}
