using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    static public int winnerID;
    public GameObject YootUI;
    public GameObject BattleUI;
    public UnitInstanceFactory allyUnitInstanceFactory;
    public UnitInstanceFactory enemyUnitInstanceFactory;
    public string AllyUnitTag = "AllyUnit";
    public string EnemyUnitTag = "EnemyUnit";

    public YootField caller;

    public CameraHandler cameraHandler;

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void StartBattle()
    {
        SetupBattle();
    }
    
    public void SetupBattle()
    {
        gameObject.SetActive(true);
        winnerID = -1;
        CreateUnits();
        cameraHandler.Backup();
        cameraHandler.GoBattleField();
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
            winnerID = 1;
            return true;
        }
        else if (GameObject.FindGameObjectsWithTag(EnemyUnitTag).Length == 0)
        {
            winnerID = 0;
            return true;
        }
        else return false;
    }

    private void FinishBattle()
    {
        DestroyUnits();
        cameraHandler.Recover();
        YootUI.SetActive(true);
        BattleUI.SetActive(false);
        gameObject.SetActive(false);
        Debug.Log("Winner: " + winnerID);
        if (caller)
        {
            caller.HandleBattlResult(winnerID);
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
