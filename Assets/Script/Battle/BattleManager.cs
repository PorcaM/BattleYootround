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
    public string AllyUnitTag = "AllyUnit";
    public string EnemyUnitTag = "EnemyUnit";

    public YootField caller;

    private Vector3 backupPos;

    void Start () {
        
	}

    public void Init()
    {
        gameObject.SetActive(true);
        winner = -1;
        CreateUnits();
        backupPos = mainCamera.transform.position;
        mainCamera.transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        YootUI.SetActive(false);
        BattleUI.SetActive(true);
    }

    private void CreateUnits()
    {
        allyUnitInstanceFactory.unitTag = AllyUnitTag;
        allyUnitInstanceFactory.CreateUnits();
        enemyUnitInstanceFactory.unitTag = EnemyUnitTag;
        enemyUnitInstanceFactory.CreateUnits();
    }

    void Update()
    {
        if (IsBattleOver())
            FinishBattle();
    }

    private bool IsBattleOver()
    {
        if (GameObject.FindGameObjectsWithTag(AllyUnitTag).Length == 0)
        {
            winner = 1;
            return true;
        }
        else if (GameObject.FindGameObjectsWithTag(EnemyUnitTag).Length == 0)
        {
            winner = 0;
            return true;
        }
        else return false;
    }

    private void FinishBattle()
    {
        DestroyUnits();
        mainCamera.transform.position = backupPos;
        YootUI.SetActive(true);
        BattleUI.SetActive(false);
        gameObject.SetActive(false);
        Debug.Log(winner);
        if (caller)
        {
            caller.HandleBattlResult(winner);
        }
    }

    private void DestroyUnits()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(AllyUnitTag);
        foreach(GameObject obj in allies)
            Destroy(obj);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyUnitTag);
        foreach (GameObject obj in enemies)
            Destroy(obj);
        GameObject[] deads = GameObject.FindGameObjectsWithTag("DeadUnit");
        foreach (GameObject obj in deads)
            Destroy(obj);
    }
}
