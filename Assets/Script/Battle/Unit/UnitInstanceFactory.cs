using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour {
    public string unitTag;
    public Equipment equipment;
    public Transform LookatPoint;
    public Transform UnitParent;
    public UnitModels unitModels;
    public float spanwPosZ;
    public Transform center;

    public const int instancePerUnit = 3;

    public void CreateUnits()
    {
        int order = 0;
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; i++)
            {
                GameObject unitModel = unitModels.models[unit.Id];
                CreateUnit(unit, unitModel, i, order);
            }
            ++order;
        }
    }

    private void CreateUnit(Unit unit, GameObject unitModel, int num, int order)
    {        
        Vector3 position = GetPosition(num, order);
        GameObject gameObject = CreateObject(unitModel, position);
        gameObject.tag = unitTag;
        UnitInstance unitInstance = gameObject.AddComponent<UnitInstance>();
        unitInstance.unitAnimation = gameObject.AddComponent<UnitAnimation>();
        unitInstance.unitHealthBar = gameObject.AddComponent<UnitHealthBar>();
        unitInstance.Init(unit);
        unitInstance.name = unit.UnitClass.ToString() + num;
    }

    private Vector3 GetPosition(int num, int row)
    {
        const float xInterval = .5f;
        const float zInterval = .3f;
        float dir = spanwPosZ / Mathf.Abs(spanwPosZ);
        float x = (num - 1) * xInterval + Random.Range(-.1f, .1f);
        float z = row * dir * zInterval + Random.Range(-.1f, .1f);
        Vector3 localPosition = new Vector3(x, 0, z);
        localPosition.z += spanwPosZ;
        Vector3 position = localPosition + center.position;
        return position;
    }

    private GameObject CreateObject( GameObject unitObject, Vector3 position)
    {
        Quaternion rotation = GetRotationLookAt(LookatPoint.position, position);
        GameObject gameObject = Instantiate(unitObject, position, rotation, UnitParent) as GameObject;
        return gameObject;
    }

    private Quaternion GetRotationLookAt(Vector3 point, Vector3 position)
    {
        Vector3 relativePos = point - position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        return rotation;
    }
}
