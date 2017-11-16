﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum RaceState { Ready, Running, Finished, Together };
    public TurnManager turnManager;
    public YootField currentLocation;
    public YootPlayer owner;

    public RaceState currentState;
    public HorseRoute.Type routeType;
    
    public void StartRunning()
    {
        if (IsReady())
            StandOnStartline();
    }

    private bool IsReady()
    {
        return currentState == RaceState.Ready;
    }

    private void StandOnStartline()
    {
        currentState = RaceState.Running;
        YootBoard.GetStartPoint().Arrive(this);
    }

    public void Move(YootGame.YootCount yootCount)
    {
        YootField destination = YootBoard.GetDestination(this, yootCount);
        currentLocation.Leave(this);
        if (destination == YootBoard.GetStartPoint())
        {
            currentState = RaceState.Finished;
            // owner.numFinished++;
            Destroy(gameObject);
        }
        else
            destination.Arrive(this);
    }

    public void Defeat()
    {
        if (owner)
        {
            // owner.createdHorses.Remove(gameObject);
            // owner.numRunnerText.text = owner.createdHorses.Count.ToString();
        }
        Destroy(gameObject);
    }

    public void Selected()
    {
        if (currentState == RaceState.Running)
            owner.turnManager.SelectHorse(this);
    }
}
