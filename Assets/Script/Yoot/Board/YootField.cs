using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    public int id;
    public List<Horse> horses;
    public Material normal;
    public Material waitSelect;
    public Horse.RunningRoute milestone;
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

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public List<Horse> Horses
    {
        get
        {
            return horses;
        }

        set
        {
            horses = value;
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
        Horses = new List<Horse>();
    }

    public void Init(int id)
    {
        Id = id;
    }

    public void Arrive(Horse horse)
    {
        horses.Add(horse);
        horse.transform.position = transform.position;
        horse.currentLocation = this;
        if (milestone != Horse.RunningRoute.Outside)
            horse.CurrentRoute = milestone;
        if (IsEncounter())
        {
            if(horses[0].tag == horses[1].tag)
            {
                horses[0].State = Horse.RaceState.Together;
                horses[1].State = Horse.RaceState.Together;
            }
            else
            {
                battleManager.caller = this;
                battleManager.Init();
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
        foreach (Horse horse in horses)
        {
            if (horse.tag == looserTag)
                horse.Defeat();
        }
    }

    private bool IsEncounter()
    {
        return horses.Count > 1;
    }

    public void Leave(Horse horse)
    {
        horses.Remove(horse);
    }

    public void Selected()
    {
        TurnManager.FieldIsSelected(this);
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
