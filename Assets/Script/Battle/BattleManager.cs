using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject enemy;
    public GameObject ally;
    public GameObject mainCamera;
    static public int winner;

    private GameObject[] enemies;
    private GameObject[] allies;

    private const int unitCount = 6;    

    void Start () {
        
	}

    public void Init()
    {
        CreateEnemies();
        CreateAllies();
        winner = -1;
        mainCamera.transform.position = new Vector3(transform.position.x, 30, transform.position.z);
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2-1, 3, 10);
            position += transform.position;
            Quaternion quaternion = new Quaternion();
            Instantiate(enemy, position, quaternion);
        }
    }

    private void CreateAllies()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Vector3 position = new Vector3(i*2-1, 3, -10);
            position += transform.position;
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
        mainCamera.transform.position = new Vector3(0, 15, 0);
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
