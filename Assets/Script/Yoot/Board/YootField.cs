using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    public int id;
    public List<Horse> guests;
    public HorseRoute.Type milestone;
    public BattleManager battleManager;

    private bool destFlag;

    public bool DestFlag
    {
        get
        {
            return destFlag;
        }

        set
        {
            destFlag = value;
            Highlight(destFlag);
        }
    }

    void Start()
    {
        guests = new List<Horse>();
        DestFlag = false;
    }

    public void Arrive(Horse horse)
    {
        guests.Add(horse);
        horse.transform.position = transform.position;
        horse.currentLocation = this;
        if (milestone != HorseRoute.Type.Summer)
            horse.routeType = milestone;
        if (IsEncounter())
        {
            if(guests[0].tag == guests[1].tag)
            {
                guests[0].RunTogether(guests[1]);
            }
            else
            {
                EnterBattle();
            }
        }
    }

    private void EnterBattle()
    {
        guests[1].owner.turnManager.currentState = TurnManager.ProcessState.WaitBattle;
        battleManager.caller = this;
        battleManager.SetupBattle();
    }

    public void HandleBattlResult(int winner)
    {
        string looserTag;
        if (winner == 0)
            looserTag = "EnemyHorse";
        else
            looserTag = "AllyHorse";
        foreach (Horse horse in guests)
        {
            if (horse.tag == looserTag)
                horse.Defeat();
        }
    }

    private bool IsEncounter()
    {
        return guests.Count > 1;
    }

    public void Leave(Horse horse)
    {
        guests.Remove(horse);
    }

    public void Highlight(bool flag)
    {
        if (flag)
        {
        }
        else
        {
        }
    }
}
