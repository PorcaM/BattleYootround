using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {
    public YootField currentLocation;
    public YootPlayer owner;
    public int id;
    public int weight;    
    public HorseRoute.Type routeType;

    private TurnProcessor turnProcessor;

    public void Init(YootPlayer owner, int id)
    {
        this.owner = owner;
        this.id = id;
        currentLocation = YootBoard.GetStartPoint();
        weight = 1;
        turnProcessor = owner.turnProcessor;
    }

    public bool IsEnemy(Horse other)
    {
        return owner.playerID != other.owner.playerID;
    }

    void OnMouseDown()
    {
        turnProcessor.RecvHorseSelect(this);
    }

    public void Move(YootGame.YootCount yootCount)
    {
        YootField destination = YootBoard.GetDestination(this, yootCount);
        currentLocation.Leave(this);
        if (destination == YootBoard.GetStartPoint())
        {
            owner.horseManager.FinishRace(this);
            Destroy(gameObject);
        }
        else
            destination.Arrive(this);
    }

    public void Defeat()
    {
        owner.horseManager.ReviveHorse(weight);
        Destroy(gameObject);
    }

    public void RunTogether(Horse partner)
    {
        weight += partner.weight;
        Destroy(partner.gameObject);
    }
}
