using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public List<GameObject> players;

    public int currentPlayer;

    public void Init()
    {
        foreach (GameObject playerObj in players)
            playerObj.GetComponent<YootPlayer>().Init();
    }
}
