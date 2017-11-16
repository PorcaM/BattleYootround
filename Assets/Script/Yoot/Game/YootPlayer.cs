using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootPlayer : MonoBehaviour {
    public GameObject horsePref;
    public GameObject horseStatusPannel;
    private Transform[] horseTokenButtons;
    private const int numHorse = 4;
    public int currentHorse;
    public Horse[] ownedHorses;

    public HorseFactory horseFactory;
    public List<GameObject> createdHorses;
    public UnityEngine.UI.Text numRunnerText;
    public int numFinished;

    void Start()
    {
        createdHorses = new List<GameObject>();
    }

    public void StartNewHorse()
    {
        if (createdHorses.Count < numHorse)
        {
            GameObject gameObject = horseFactory.CreateHorse();
            gameObject.GetComponent<Horse>().Init();
            gameObject.GetComponent<Horse>().StartRunning();
            gameObject.GetComponent<Horse>().yootPlayer = this;
            createdHorses.Add(gameObject);
            numRunnerText.text = createdHorses.Count.ToString();
        }
        else
            return;
    }

    public void JudgeGameResult()
    {
        Debug.Log(numFinished);
        if(numFinished == 4)
        {
            Win();
        }
    }

    public void Win()
    {

    }
}
