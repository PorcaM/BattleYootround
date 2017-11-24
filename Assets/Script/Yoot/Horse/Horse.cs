using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public YootField currField;
    private YootPlayer owner;
    public int id;
    public int weight;
    public HorseRoute.Type routeType;

    private TurnProcessor turnProcessor;
    public HorseManager horseManager;

    public void Init(YootPlayer owner, int id)
    {
        this.owner = owner;
        this.id = id;
        weight = 1;
        currField = YootBoard.GetStartPoint();
        turnProcessor = owner.turnProcessor;
        horseManager = owner.horseManager;
    }

    public bool IsOwner(int player)
    {
        return player == owner.playerID;
    }

    public bool IsStandby()
    {
        return currField.IsGoal();
    }

    public bool IsEnemyWith(Horse other)
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

    public void CarryHorse(Horse partner)
    {
        weight += partner.weight;
        Destroy(partner.gameObject);
    }

    void OnDestroy()
    {
        currField.Leave(this);
    }
}
