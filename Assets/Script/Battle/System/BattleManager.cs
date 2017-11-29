using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public enum BattleState { Inited, Ready, Proceeding }
    public BattleState battleState;
    static public int winPlayer;
    public UnitInstanceFactory allyUnitInstanceFactory;
    public UnitInstanceFactory enemyUnitInstanceFactory;
    public string AllyUnitTag = "AllyUnit";
    public string EnemyUnitTag = "EnemyUnit";

    public YootField battleField;

    public CameraHandler cameraHandler;
    public UIHandler uiHandler;

    public FloatingText floatingText;
    public Transform damagesParent;
    public SpellManager spellManager;
    public ObjController objController;

    public YootGame yootGame;

    public EnterBattleDecorator enterBattleDecorator;
    public ExitBattleDecorator exitBattleDecorator;

    public void Init()
    {
        gameObject.SetActive(false);
        spellManager.Init();
        FloatingTextController.Init(floatingText, damagesParent);
        cameraHandler.Init();
        battleState = BattleState.Inited;
    }

    public void StartBattle()
    {
        SetupBattle();
    }

    public void StartBattle(YootField battleField)
    {
        this.battleField = battleField;
        SetupBattle();
    }
    
    private void SetupBattle()
    {
        winPlayer = -1;
        gameObject.SetActive(true);
        CreateUnits();
        cameraHandler.GoBattleField();
        uiHandler.SetUIActive(true);
        battleState = BattleState.Ready;
    }

    private void CleanupBattle()
    {
        uiHandler.SetUIActive(false);
        cameraHandler.Recover();
        DestroyUnits();
        spellManager.Cleanup();
        gameObject.SetActive(false);
    }

    private void CreateUnits()
    {
        allyUnitInstanceFactory.unitTag = AllyUnitTag;
        allyUnitInstanceFactory.spanwPosZ = -1.0f;
        allyUnitInstanceFactory.equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        allyUnitInstanceFactory.CreateUnits();
        enemyUnitInstanceFactory.unitTag = EnemyUnitTag;
        enemyUnitInstanceFactory.spanwPosZ = 1.0f;
        // TODO Change this to EnemyEquipment
        enemyUnitInstanceFactory.equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
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
        if (battleField)
        {
            battleField.RecvBattlResult(winPlayer);
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
