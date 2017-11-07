using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public TurnManager turnManager;

    public void Move(YootGame.YootCount yootCount)
    {
        Debug.Log("Horse.Move()");
    }

    public void Selected()
    {
        turnManager.SelectedHorse = this;
        turnManager.CurrentState = TurnManager.ProcessState.MoveHorse;
    }
}
