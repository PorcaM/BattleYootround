using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class DecoTurnStart : MonoBehaviour {
    public static void ShowStarter(int playerID)
    {
        string content = "Player " + playerID + " starts turn!";
        ToastManager.Show(content);
    }
}
