using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum RaceState { Ready, Running, Finished };
    public enum RunningRoute { Outside, Vertical, Horizon, Shortest };
    public TurnManager turnManager;
    public YootPlayer yootPlayer;
    public YootField currentLocation;
    public Transform button;

    private RaceState state;
    private RunningRoute runningRoute;

    public RaceState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
            UpdateStateButton();
            if (yootPlayer)
                yootPlayer.JudgeGameResult();
        }
    }

    public TurnManager TurnManager
    {
        get
        {
            if (turnManager == null)
            {
                turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
            }
            return turnManager;
        }
    }

    public RunningRoute RunningRoute1
    {
        get
        {
            return runningRoute;
        }

        set
        {
            runningRoute = value;
        }
    }

    private void UpdateStateButton()
    {
        if (button)
        {
            button.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = state.ToString();
        }
    }

    public void Init(YootPlayer yootPlayer)
    {
        State = RaceState.Ready;
        this.yootPlayer = yootPlayer;
    }

    void Update()
    {

    }

    public void StartRunning()
    {
        State = RaceState.Running;
        RunningRoute1 = RunningRoute.Outside;
        YootBoard.GetStartPoint().Arrive(this);
    }

    public void Move(YootGame.YootCount yootCount)
    {
        YootField destination = YootBoard.GetDestination(currentLocation, yootCount);
        currentLocation.Leave(this);
        if (destination == YootBoard.GetStartPoint())
        {
            State = RaceState.Finished;
            transform.position = new Vector3(3, 0, -6);
        }
        else
            destination.Arrive(this);
    }

    public void Selected()
    {
        if (state != RaceState.Running)
            return;
        TurnManager.SelectedHorse = this;
        TurnManager.CurrentState = TurnManager.ProcessState.MoveHorse;
    }
}
