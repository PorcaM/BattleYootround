using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    public int id;
    public List<Horse> horses;
    public Material normal;
    public Material waitSelect;

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
    }

    public void Leave(Horse horse)
    {
        horses.Remove(horse);
    }

    public void Selected()
    {
        TurnManager.FieldIsSelected(this);
    }

    public void WaitSelect()
    {
        transform.GetChild(0).GetComponent<Renderer>().material = waitSelect;
    }

    public void NotWaitSelect()
    {
        transform.GetChild(0).GetComponent<Renderer>().material = normal;
    }
}
