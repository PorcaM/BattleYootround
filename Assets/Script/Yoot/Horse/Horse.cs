using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum RaceState { Ready, Running, Finished, Together };
    public enum RunningRoute { Outside, Horizon, Vertical, Shortest };
    public TurnManager turnManager;
    public YootField currentLocation;
    public Transform button;
    public YootPlayer yootPlayer;

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

    public void Init()
    {
        State = RaceState.Ready;
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
            yootPlayer.numFinished++;
            Destroy(gameObject);
        }
        else
            destination.Arrive(this);
    }

    public void Defeat()
    {
        if (yootPlayer)
        {
            yootPlayer.createdHorses.Remove(gameObject);
            yootPlayer.numRunnerText.text = yootPlayer.createdHorses.Count.ToString();
        }
        Destroy(gameObject);
    }

    public void Selected()
    {
        if (state != RaceState.Running)
            return;
        TurnManager.HorseIsSelected(this);
    }
}
