using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoCreater : MonoBehaviour {
    public GameInfo original;

    public void CreateLocal()
    {
        GameInfo gameInfo = Instantiate(original);
        gameInfo.name = "GameInfo";
        gameInfo.gameMode = YootGame.GameMode.Local;
        DontDestroyOnLoad(gameInfo);
    }

    public void CreateNetwork()
    {
        GameInfo gameInfo = Instantiate(original);
        gameInfo.name = "GameInfo";
        gameInfo.gameMode = YootGame.GameMode.Network;
        DontDestroyOnLoad(gameInfo);
    }
}
