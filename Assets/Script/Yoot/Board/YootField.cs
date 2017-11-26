using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour
{
    public int id;
    public List<Horse> guests;
    public HorseRoute.Type milestone;

    private BattleManager battleManager;
    private DecoEnterBattle deco;

    public void Init(int id, BattleManager battleManager)
    {
        this.id = id;
        this.battleManager = battleManager;
        guests = new List<Horse>();
        milestone = HorseRoute.Type.Summer;
        deco = GameObject.Find("Decos").GetComponent<DecoEnterBattle>();
        deco.action = new System.Action(EnterBattle);
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
        else
            RequestTurnEnd(horse);
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
            ReadyBattle();
        else
        {
            horse.CarryHorse(Guest(0));
            RequestTurnEnd(horse);
        }
    }

    private void RequestTurnEnd(Horse horse)
    {
        horse.owner.turnProcessor.RecvEnd();
    }

    private void ReadyBattle()
    {
        const float timerInterval = 1.5f;
        deco.action = new System.Action(EnterBattle);
        deco.OnEnterBattle(timerInterval);
    }

    private void EnterBattle()
    {
        battleManager.StartBattle(this);
    }

    public void RecvBattlResult(int winner)
    {
        Debug.Log("winner:" + winner);
        foreach (Horse horse in guests)
        {
            Debug.Log("ownerid: " + horse.owner.playerID);
            if (horse.IsOwner(winner) == false)
            {
                Debug.Log("Defeat:" + horse.id);
                horse.Defeat();
                RequestTurnEnd(horse);
            }
        }
    }
}
