using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootGame : MonoBehaviour {
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo };
    public TurnManager turnManager;

    void Update()
    {
        if (turnManager.CurrentState == TurnManager.ProcessState.NotMyTurn)
            turnManager.CurrentState = TurnManager.ProcessState.Throw;
    }
}
