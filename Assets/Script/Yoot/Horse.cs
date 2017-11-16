using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum RaceState { Ready, Running, Finished, Together };
    public enum RunningRoute { Outside, Horizon, Vertical, Shortest };
    public TurnManager turnManager;
    public YootPlayer yootPlayer;
    public YootField currentLocation;
    public Transform button;

    private RaceState state;
    
    public RunningRoute currentRoute;

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

    public RunningRoute CurrentRoute
    {
        get
        {
            return currentRoute;
        }

        set
        {
            currentRoute = value;
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

    public void StartRunning()
    {
        if (State == RaceState.Ready)
        {
            State = RaceState.Running;
            CurrentRoute = RunningRoute.Outside;
            YootBoard.GetStartPoint().Arrive(this);
        }
    }

    public void Move(YootGame.YootCount yootCount)
    {
        YootField destination = YootBoard.GetDestination(this, yootCount);
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
        TurnManager.HorseIsSelected(this);
    }
}
