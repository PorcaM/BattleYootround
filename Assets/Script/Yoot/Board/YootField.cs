using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    public int id;
    public List<Horse> guests;
    public HorseRoute.Type milestone;
    public BattleManager battleManager;
    private bool destFlag;

    private TurnManager turnManager;
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
                battleManager.caller = this;
                battleManager.SetupBattle();
            }
        }
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

    public void Selected()
    {
        TurnManager.SelectField(this);
    }

    public void Highlight(bool flag)
    {
        if (flag)
        {
            cakeslice.Outline outline = GetComponent<cakeslice.Outline>();
            outline.eraseRenderer = false;
            outline.color = 1;
        }
        else
        {
            cakeslice.Outline outline = GetComponent<cakeslice.Outline>();
            outline.eraseRenderer = true;
        }
    }
}
