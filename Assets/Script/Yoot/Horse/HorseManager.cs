using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : MonoBehaviour {
    public YootPlayer owner;
    public HorseFactory horseFactory;
    public List<GameObject> runners;
    public int numFinished;
    public int maxNumRunner = 4;

    public void StartNewHorse()
    {
        if (IsMoreRunner())
            AddNewRunner();
    }

    private bool IsMoreRunner()
    {
        return runners.Count < maxNumRunner;
    }

    private void AddNewRunner()
    {
        GameObject gameObject = horseFactory.CreateHorse();
        runners.Add(gameObject);
    }

    public bool IsGameOver()
    {
        return numFinished >= maxNumRunner;
    }
}
