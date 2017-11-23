﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootPlayer : MonoBehaviour {
    private const int numHorse = 4;

    public YootGame yootGame;
    public HorseFactory horseFactory;
    public HorseManager horseManager;
    public TurnProcessor turnManager;
    public string tagPrefix;
    public int playerID;

    public void Init()
    {
        horseManager.Init();
    }

    public void StartNewHorse()
    {
        horseManager.StartNewHorse();
    }

    public void Win()
    {
        Debug.Log("Player " + playerID + " Win!!");
    }

    public void Register(Horse horse)
    {

    }
}
