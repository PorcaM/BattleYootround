using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootPlayer : MonoBehaviour {
    public GameObject horsePref;
    public GameObject horseStatusPannel;
    private Transform[] horseTokenButtons;
    private const int numHorse = 4;
    public Horse[] ownedHorses;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        Vector3 position = new Vector3(0, 0, -6);
        for (int i = 0; i < numHorse; ++i)
        {
            Instantiate(horsePref, position, Quaternion.identity, transform);
        }
        ownedHorses = new Horse[numHorse];
        for(int i = 0; i < numHorse; ++i)
        {
            ownedHorses[i] = transform.GetChild(i).GetComponent<Horse>();
            ownedHorses[i].Init(this);
        }
        horseTokenButtons = new Transform[numHorse];
        for (int i = 0; i < numHorse; ++i)
        {
            horseTokenButtons[i] = horseStatusPannel.transform.GetChild(i);
            ownedHorses[i].button = horseTokenButtons[i];
            horseTokenButtons[i].GetChild(0).GetComponent<UnityEngine.UI.Text>().text = ownedHorses[i].State.ToString();
            horseTokenButtons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ownedHorses[i].StartRunning);
        }
    }

    public void JudgeGameResult()
    {
        bool win = true;
        for (int i = 0; i < numHorse; ++i)
        {
            if (ownedHorses[i].State != Horse.RaceState.Finished)
            {
                win = false;
            }
        }
        if (win)
        {
            YootGame.Win();
        }
    }

    public void Win()
    {

    }
}
