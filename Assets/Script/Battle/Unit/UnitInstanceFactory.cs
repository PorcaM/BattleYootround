using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour {
    public string unitTag;
    public Equipment equipment;
    public Transform spawnPoint;
    public Transform LookatPoint;
    public Transform UnitParent;
    public UnitModels unitModels;

    public const int instancePerUnit = 3;

    public void CreateUnits()
    {
        equipment.TempInit();
        Debug.Log(equipment.ToString());
        
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; i++)
            {
                GameObject unitModel = unitModels.models[unit.Id];
                CreateUnit(unit, unitModel, i);
            }
        }
    }

    private void CreateUnit(Unit unit, GameObject unitObject, int num)
    {
        Vector3 position = GetPosition(num, unit.Id); // TODO 위치로 바꿔야 함
        GameObject gameObject = CreateObject(unitObject, position);
        gameObject.tag = unitTag;        
        UnitInstance unitInstance = gameObject.AddComponent<UnitInstance>();
        unitInstance.unitAnimation = gameObject.AddComponent<UnitAnimation>();
        unitInstance.unitHealthBar = gameObject.AddComponent<UnitHealthBar>();
        unitInstance.Init(unit);
    }

    private Vector3 GetPosition(int num, int row)
    {
        Vector3 localPosition = new Vector3(-1 + num, 0, row);
        Vector3 position = spawnPoint.position + localPosition;
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
