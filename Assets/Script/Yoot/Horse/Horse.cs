using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public enum RaceState { Ready, Running, Finished, Together };
    public YootField currentLocation;
    public YootPlayer owner;
    public int id;
    public int weight;

    public RaceState currentState;
    public HorseRoute.Type routeType;

    public void Init()
    {
        currentLocation = YootBoard.GetStartPoint();
        weight = 1;
    }
    
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
        Debug.Log("dst id " + destination.id);
        if (destination == YootBoard.GetStartPoint())
        {
            currentState = RaceState.Finished;
            owner.horseManager.FinishRace(this);
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
        owner.turnManager.SelectHorse(this);
    }

    public void RunTogether(Horse partner)
    {
        weight += partner.weight;
        Destroy(partner.gameObject);
    }
}
