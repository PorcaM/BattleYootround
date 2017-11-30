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

    }

    public void Setup()
    {
        CreateUnits();
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
