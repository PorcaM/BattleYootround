using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public List<GameObject> players;

    public void Init()
    {
        InitPlayers();
    }

    private void InitPlayers()
    {
        foreach (GameObject playerObj in players)
            playerObj.GetComponent<YootPlayer>().Init();
    }

    public YootPlayer GetPlayer(int index)
    {
        return players[index].GetComponent<YootPlayer>();
    }

    public int NumPlayer()
    {
        return players.Count;
    }

    public int GetNextPlayer(int currentPlayer)
    {
        return (currentPlayer + 1) % NumPlayer();
    }
}
