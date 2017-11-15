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
                battleManager.Init();
            }
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
            transform.GetChild(0).GetComponent<Renderer>().material = waitSelect;
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material = normal;

        }
    }
}
