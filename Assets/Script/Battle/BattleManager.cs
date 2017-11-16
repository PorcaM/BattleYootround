using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public GameObject mainCamera;
    static public int winner;
    public GameObject YootUI;
    public GameObject BattleUI;
    public UnitInstanceFactory allyUnitInstanceFactory;
    public UnitInstanceFactory enemyUnitInstanceFactory;

    private GameObject[] enemies;
    private GameObject[] allies;

    void Start () {
        
	}

    public void Init()
    {
        gameObject.SetActive(true);
        allyUnitInstanceFactory.CreateUnits("AllyUnit");
        enemyUnitInstanceFactory.CreateUnits("EnemyUnit");
        winner = -1;
        mainCamera.transform.position = new Vector3(transform.position.x, 30, transform.position.z);
        YootUI.SetActive(false);
        BattleUI.SetActive(true);
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
        YootUI.SetActive(true);
        YootUI.SetActive(false);
        gameObject.SetActive(false);
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
