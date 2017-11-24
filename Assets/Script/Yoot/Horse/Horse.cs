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
    private HorseManager horseManager;

    public void Init(YootPlayer owner, int id)
    {
        this.owner = owner;
        this.id = id;
        weight = 1;
        currentLocation = YootBoard.GetStartPoint();
        turnProcessor = owner.turnProcessor;
        horseManager = owner.horseManager;
    }

    public bool IsEnemy(Horse other)
    {
        return owner.playerID != other.owner.playerID;
    }

    void OnMouseDown()
    {
        turnProcessor.RecvHorseSelect(this);
    }

    public void GoalIn()
    {
        horseManager.RecvGoalIn(this);
        Destroy(gameObject);
    }

    public void Defeat()
    {
        Destroy(gameObject);
    }

    public void RunTogether(Horse partner)
    {
        weight += partner.weight;
        Destroy(partner.gameObject);
    }
}
