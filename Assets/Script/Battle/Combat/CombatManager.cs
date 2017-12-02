using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public enum State { Idle, Setup, Standby, OnBattle }
    public State state;

    public CameraHandler cameraHandler;
    public FloatingText floatingText;
    public Transform damagesParent;
    public UnitManager unitManager;

    private static CombatManager instance;
    public static CombatManager Instance()
    {
        if (!instance)
            instance = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        return instance;
    }

    private int winner;

    public void Init()
    {
        FloatingTextController.Init(floatingText, damagesParent);
        UnitHealthBar.Init();
        cameraHandler.Init();
        unitManager.Init();
        state = State.Idle;
    }

    public void Setup()
    {
        unitManager.Setup();
        cameraHandler.Setup();
        state = State.Setup;
    }

    public void StartBattle()
    {
        unitManager.SetAllUnitState(UnitInstance.State.Alive);
        state = State.OnBattle;
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
            winner = 1;
            return true;
        }
        else if (unitManager.EnemyUnitCount() == 0)
        {
            winner = 0;
            return true;
        }
        else
            return false;
    }

    private void FinishBattle()
    {
        Debug.Log("Battle over");
        BattleGame battleGame = BattleGame.Instance();
        battleGame.FinishBattle(winner);
    }

    public void CleanupBattle()
    {
        cameraHandler.Cleanup();
        unitManager.Cleanup();
    }
}
