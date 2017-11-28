using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseManager : MonoBehaviour {
    public YootPlayer owner;
    public HorseFactory horseFactory;
    public Slider slider;

    [SerializeField] private List<Horse> runners;
    public int numFinished;
    public int maxNumRunner = 4;

    public Horse standbyHorse;
    public Transform standbyLocation;

    private int lastId;

    public void Init()
    {
        numFinished = 0;
        lastId = -1;
        NewStandbyRunner();
    }

    public void NewStandbyRunner()
    {
        Horse horse = horseFactory.CreateHorse(owner, ++lastId);
        horse.GetComponent<Horse>().id = lastId;
        horse.name = owner.playerID + " Horse " + lastId;
        horse.transform.position = standbyLocation.position;
        runners.Add(horse);
    }    

    public void RecvGoalIn(Horse horse)
    {
        numFinished += horse.weight;
        slider.value = numFinished;
        if (IsAchieveVictory())
            owner.Win();
    }

    private bool IsAchieveVictory()
    {
        return numFinished >= maxNumRunner;
    }

    public void RecvDestroy(Horse horse)
    {
        runners.Remove(horse);
    }

    public void SetClickable(bool flag)
    {
        foreach(Horse horse in runners)
        {
            horse.GetComponent<MeshCollider>().enabled = flag;
        }
    }

    public void RemoveHorse(Horse horse)
    {
        runners.Remove(horse);
        Destroy(horse.gameObject);
    }
}
