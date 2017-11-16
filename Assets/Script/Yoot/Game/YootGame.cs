using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootGame : MonoBehaviour {
    public enum Mode { Solo, Network };
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = -1 };
    public TurnManager turnManager;
    public GameObject enemyHorse;

    private Mode mode;

    void Awake()
    {
        Screen.SetResolution(720, 1280, true);
    }

    void Start()
    {
        mode = Mode.Solo;
        HorseRoute.Init();
    }

    public void TestEnemyHorse()
    {
        GameObject horse = Instantiate(enemyHorse) as GameObject;
        YootBoard.Fields[10].GetComponent<YootField>().Arrive(horse.GetComponent<Horse>());
    }

    void Update()
    {
        if (mode == Mode.Solo)
        {
            if (turnManager.CurrentState == TurnManager.ProcessState.WaitTurn)
                turnManager.StartTurn();
        }
    }

    public static void Win()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }
}
