﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseFactory : MonoBehaviour {
    public GameObject horseModel;
    public Transform horseParent;

    public GameObject CreateHorse(YootPlayer owner, int id)
    {
        GameObject horseObj = Instantiate(horseModel, horseParent) as GameObject;
        Horse horse = horseObj.GetComponent<Horse>();
        horse.Init(owner, id);
        return horseObj;
    }
}
