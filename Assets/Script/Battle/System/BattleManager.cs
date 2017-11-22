using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    static public int winnerID;
    public UnitInstanceFactory allyUnitInstanceFactory;
    public UnitInstanceFactory enemyUnitInstanceFactory;
    public string AllyUnitTag = "AllyUnit";
    public string EnemyUnitTag = "EnemyUnit";

    public YootField caller;

    public CameraHandler cameraHandler;
    public UIHandler uiHandler;

    public FloatingText floatingText;
    public Transform damagesParent;
    public SpellManager spellManager;

    public YootGame yootGame;

    public void Init()
    {
        gameObject.SetActive(false);
        FloatingTextController.Init(floatingText, damagesParent);
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
        uiHandler.SetUIActive(true);
        spellManager.Init();
    }

    public void CleanupBattle()
    {
        uiHandler.SetUIActive(false);
        cameraHandler.Recover();
        DestroyUnits();
        gameObject.SetActive(false);
    }

    private void CreateUnits()
    {
        allyUnitInstanceFactory.unitTag = AllyUnitTag;
        allyUnitInstanceFactory.spanwPosZ = 3.0f;
        allyUnitInstanceFactory.CreateUnits();
        enemyUnitInstanceFactory.unitTag = EnemyUnitTag;
        enemyUnitInstanceFactory.spanwPosZ = -3.0f;
        enemyUnitInstanceFactory.CreateUnits();
    }

    void Update()
    {
        CheckBattleOver();
    }

    public void CheckBattleOver()
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
        else
            return false;
    }

    private void FinishBattle()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag(AllyUnitTag).Length);
        Debug.Log(GameObject.FindGameObjectsWithTag(EnemyUnitTag).Length);
        CleanupBattle();
        Debug.Log("Winner: " + winnerID);
        if (caller)
        {
            caller.HandleBattlResult(winnerID);
            yootGame.HandleBattleResult(winnerID);
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
