using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    private int id;
    private List<Horse> horses;

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
        Id = -1;
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
}
