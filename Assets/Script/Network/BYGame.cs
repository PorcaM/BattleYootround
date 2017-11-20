using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BYGame : MonoBehaviour {
    public static void OnGame(Pair<int, int> player)
    {
        // 매칭완료 메세지 전달
        BYServer.SendClient(player.First, "Match Success!");
        BYServer.SendClient(player.Second, "Match Success!");

        /*
        int turn;

        // 클라이언트들 씬 전환(Yoot) 후 게임 끝날때 까지 while
        while(status != End)
        {
            // Yoot씬 상태일 때
            if(status == Yoot)
            {

            }
            // Battle 씬 상태일 때
            else if(status == Battle)
            {

            }

        }
        */
    }
}
