﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    static public int winPlayer;
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
        spellManager.Init();
        FloatingTextController.Init(floatingText, damagesParent);
    }

    public void StartBattle()
    {
        SetupBattle();
    }
    
    public void SetupBattle()
    {
        winPlayer = -1;
        gameObject.SetActive(true);
        CreateUnits();
        cameraHandler.Backup();
        cameraHandler.GoBattleField();
        uiHandler.SetUIActive(true);
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
        allyUnitInstanceFactory.spanwPosZ = -3.0f;
        allyUnitInstanceFactory.CreateUnits();
        enemyUnitInstanceFactory.unitTag = EnemyUnitTag;
        enemyUnitInstanceFactory.spanwPosZ = 3.0f;
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
            winPlayer = 1;
            return true;
        }
        else if (GameObject.FindGameObjectsWithTag(EnemyUnitTag).Length == 0)
        {
            winPlayer = 0;
            return true;
        }
        else
            return false;
    }

    private void FinishBattle()
    {
        CleanupBattle();
        if (caller)
        {
            caller.HandleBattlResult(winPlayer);
            yootGame.HandleBattleResult(winPlayer);
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
