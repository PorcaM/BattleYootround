using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject enemy;
    public GameObject ally;
    public int winner = -1;

    private GameObject[] enemies;
    private GameObject[] allies;

    private const int unitCount = 3;


    // Use this for initialization
    void Start () {
        CreateEnemies();
        CreateAllies();
	}

    private void CreateEnemies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2, 3, 10);
            Quaternion quaternion = new Quaternion();
            Instantiate(enemy, position, quaternion);
        }
    }

    private void CreateAllies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2, 3, -10);
            Quaternion quaternion = new Quaternion();
            Instantiate(ally, position, quaternion);
        }
    }

    // Update is called once per frame
    void Update () {
        IdentifyUnits();
        if (IsBattleOver())
            winner = GetWinner();
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
