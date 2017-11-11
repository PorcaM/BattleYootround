using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum State { Ready, Running, Finished };
    public enum RunningRoute { Outside, Vertical, Horizon, Shortest };
    public TurnManager turnManager;
    public YootField currentLocation;

    private State state;
    private RunningRoute runningRoute;

    public void Init()
    {
        state = State.Ready;
    }

    void Update()
    {

    }

    public void StartRunning()
    {
        state = State.Running;
        runningRoute = RunningRoute.Outside;
        YootBoard.GetStartPoint().Arrive(this);
    }

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
