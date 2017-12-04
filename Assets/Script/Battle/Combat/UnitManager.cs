using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public UnitInstanceFactory allyUnitInstanceFactory;
    public UnitInstanceFactory enemyUnitInstanceFactory;
    public string AllyUnitTag = "AllyUnit";
    public string EnemyUnitTag = "EnemyUnit";

    public void Init()
    {
        allyUnitInstanceFactory.unitTag = AllyUnitTag;
        allyUnitInstanceFactory.spanwPosZ = -1.0f;
        allyUnitInstanceFactory.equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        enemyUnitInstanceFactory.unitTag = EnemyUnitTag;
        enemyUnitInstanceFactory.spanwPosZ = 1.0f;
        // TODO Change this to EnemyEquipment
        enemyUnitInstanceFactory.equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
    }

    public void Setup(BYMessage.UnitPositionMessage msg = default(BYMessage.UnitPositionMessage))
    {
        if (!YootGame.isNetwork)
            CreateUnits();
        else
            CreateUnits(msg);
    }

    private void CreateUnits(BYMessage.UnitPositionMessage msg = default(BYMessage.UnitPositionMessage))
    {
        if (!YootGame.isNetwork)
        {
            allyUnitInstanceFactory.CreateUnits();
            enemyUnitInstanceFactory.CreateUnits();
        }
        else
        {
            allyUnitInstanceFactory.CreateUnits(msg.ally_pos);
            enemyUnitInstanceFactory.CreateUnits(msg.enemy_pos);
        }
    }

    public void Cleanup()
    {
        DestroyUnits();
    }

    private void DestroyUnits()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(AllyUnitTag);
        foreach (GameObject obj in allies)
            Destroy(obj);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyUnitTag);
        foreach (GameObject obj in enemies)
            Destroy(obj);
        GameObject[] deads = GameObject.FindGameObjectsWithTag("DeadUnit");
        foreach (GameObject obj in deads)
            Destroy(obj);
    }

    public void SetAllUnitState(UnitInstance.State state)
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(AllyUnitTag);
        foreach (GameObject obj in allies)
            obj.GetComponent<UnitInstance>().currentState = state;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyUnitTag);
        foreach (GameObject obj in enemies)
            obj.GetComponent<UnitInstance>().currentState = state;
    }

    public int AllyUnitCount()
    {
        return GameObject.FindGameObjectsWithTag(AllyUnitTag).Length;
    }

    public int EnemyUnitCount()
    {
        return GameObject.FindGameObjectsWithTag(EnemyUnitTag).Length;
    }
}
