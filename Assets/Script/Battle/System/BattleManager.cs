using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public enum BattleState { Inited, Ready, Proceeding }
    public BattleState battleState;

    public CameraHandler cameraHandler;
    public UIHandler uiHandler;

    public FloatingText floatingText;
    public Transform damagesParent;

    public UnitManager unitManager;

    public void Init()
    {
        FloatingTextController.Init(floatingText, damagesParent);
        cameraHandler.Init();
        unitManager.Init();
        battleState = BattleState.Inited;
    }

    public void StartBattle(YootField battleField)
    {
        StartBattle();
    }

    public void StartBattle()
    {
        SetupBattle();
        
        StartCoroutine(RealStartAfter(3.0f));
    }

    private void SetupBattle()
    {
        unitManager.Setup();
        cameraHandler.Setup();
        uiHandler.SetUIActive(true);
        battleState = BattleState.Ready;
    }

    private IEnumerator RealStartAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        RealStartBattle();
    }

    private void RealStartBattle()
    {
        unitManager.SetAllUnitState(UnitInstance.State.Alive);
        battleState = BattleState.Proceeding;
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
        if (unitManager.AllyUnitCount() == 0)
        {
            return true;
        }
        else if (unitManager.EnemyUnitCount() == 0)
        {
            return true;
        }
        else
            return false;
    }

    private void FinishBattle()
    {        
        StartCoroutine(RealFinishtAfter(3.0f));
    }

    private IEnumerator RealFinishtAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        RealFinishBattle();
    }

    private void RealFinishBattle()
    {
        CleanupBattle();
    }

    private void CleanupBattle()
    {
        uiHandler.SetUIActive(false);
        cameraHandler.Cleanup();
        unitManager.Cleanup();
        gameObject.SetActive(false);
    }
}
