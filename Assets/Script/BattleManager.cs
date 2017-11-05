using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject enemy;
    public GameObject ally;
    public int winner = -1;

    private GameObject[] enemies;
    private GameObject[] allies;

    private const int unitCount = 6;

    void Start () {
        CreateEnemies();
        CreateAllies();
	}

    private void CreateEnemies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2-1, 3, 10);
            Quaternion quaternion = new Quaternion();
            Instantiate(enemy, position, quaternion);
        }
    }

    private void CreateAllies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2-1, 3, -10);
            Quaternion quaternion = new Quaternion();
            Instantiate(ally, position, quaternion);
        }
    }


    void Update()
    {
        IdentifyUnits();
        if (IsBattleOver())
            FinishBattle();
    }

    private void IdentifyUnits()
    {
        allies = GameObject.FindGameObjectsWithTag("AllyUnit");
        enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
    }

    private bool IsBattleOver()
    {
        if (allies.Length == 0 || enemies.Length == 0)
            return true;
        else
            return false;
    }

    private void FinishBattle()
    {
        winner = GetWinner();
        // TODO 위너 값을 윷 씬에 전달해줘야 함
        UnityEngine.SceneManagement.SceneManager.LoadScene("Yoot");
    }

    private int GetWinner()
    {
        int winner = -1;
        if (allies.Length == 0)
            winner = 1;
        if (enemies.Length == 0)
            winner = 0;
        return winner;
    }
}
