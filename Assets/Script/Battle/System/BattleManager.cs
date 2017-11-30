using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public enum BattleState { Inited, Ready, Proceeding }
    public BattleState battleState;
    static public int winPlayer;

    public YootField battleField;

    public CameraHandler cameraHandler;
    public UIHandler uiHandler;

    public FloatingText floatingText;
    public Transform damagesParent;
    public SpellManager spellManager;
    public ObjController objController;

    public YootGame yootGame;

    public UnitManager unitManager;

    public EnterBattleDecorator enterBattleDecorator;
    public ExitBattleDecorator exitBattleDecorator;

    public void Init()
    {
        gameObject.SetActive(false);
        spellManager.Init();
        FloatingTextController.Init(floatingText, damagesParent);
        cameraHandler.Init();
        unitManager.Init();
        battleState = BattleState.Inited;
    }

    public void StartBattle(YootField battleField)
    {
        this.battleField = battleField;
        StartBattle();
    }

    public void StartBattle()
    {
        SetupBattle();
        enterBattleDecorator.ShowCountdown(3);
        StartCoroutine(RealStartAfter(3.0f));
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
    
    private void SetupBattle()
    {
        winPlayer = -1;
        gameObject.SetActive(true);
        unitManager.Setup();
        cameraHandler.Setup();
        uiHandler.SetUIActive(true);
        battleState = BattleState.Ready;
    }

    private void CleanupBattle()
    {
        uiHandler.SetUIActive(false);
        cameraHandler.Cleanup();
        unitManager.Cleanup();
        spellManager.Cleanup();
        gameObject.SetActive(false);
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
            winPlayer = 1;
            return true;
        }
        else if (unitManager.EnemyUnitCount() == 0)
        {
            winPlayer = 0;
            return true;
        }
        else
            return false;
    }

    private void FinishBattle()
    {
        exitBattleDecorator.ShowVictory(winPlayer);
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
        if (battleField)
        {
            battleField.RecvBattlResult(winPlayer);
            yootGame.HandleBattleResult(winPlayer);
        }
    }
}
