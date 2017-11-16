using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : MonoBehaviour {
    public YootPlayer owner;
    public HorseFactory horseFactory;
    public List<GameObject> runners;
    public int numFinished;
    public int maxNumRunner = 4;
    public Transform standbyPosition;

    public void SetupHorses()
    {
        for (int i = 0; i < maxNumRunner; ++i)
        {
            GameObject horseObj = horseFactory.CreateHorse();
            horseObj.name = owner.playerID + " Horse " + i;
            Vector3 localPosition = new Vector3(i, 0, 0);
            horseObj.transform.position = standbyPosition.position + localPosition;
            runners.Add(horseObj);
        }
    }

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
