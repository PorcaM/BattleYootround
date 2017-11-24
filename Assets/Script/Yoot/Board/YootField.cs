using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour
{
    public int id;
    public List<Horse> guests;
    public HorseRoute.Type milestone;
    public BattleManager battleManager;

    public void Init(int id, BattleManager battleManager)
    {
        this.id = id;
        this.battleManager = battleManager;
        guests = new List<Horse>();
        milestone = HorseRoute.Type.Summer;
    }

    public bool IsEmpty()
    {
        return guests.Count == 0;
    }

    public bool IsGoal()
    {
        return this == YootBoard.GetStartPoint();
    }

    public Horse Guest(int i)
    {
        return guests[i];
    }

    public void Leave(Horse horse)
    {
        guests.Remove(horse);
    }

    public void Arrive(Horse horse)
    {
        CheckIn(horse);
        SetMilestone(horse);
        if (IsEncounter())
            HandleEncounter(horse);
    }

    private void CheckIn(Horse horse)
    {
        guests.Add(horse);
        horse.currField = this;
    }

    private void SetMilestone(Horse horse)
    {
        if (milestone != HorseRoute.Type.Summer)
            horse.routeType = milestone;
    }

    private bool IsEncounter()
    {
        return guests.Count > 1;
    }

    private void HandleEncounter(Horse horse)
    {
        if (horse.IsEnemyWith(Guest(0)))
            EnterBattle();
        else
            horse.CarryHorse(Guest(0));
    }

    private void EnterBattle()
    {
        battleManager.caller = this;
        battleManager.StartBattle();
    }

    public void RecvBattlResult(int winner)
    {
        foreach (Horse horse in guests)
            if (!horse.IsOwner(winner))
                horse.Defeat();
    }
}
