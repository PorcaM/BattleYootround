using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour {
    public Equipment equipment;
    public Transform spawnPoint;
    public Transform UnitParent;
    public const int instancePerUnit = 3;

    public string unitTag;
    
    public GameObject[] units = new GameObject[7];

    public void CreateUnits()
    {
        equipment.TempInit();
        Debug.Log(equipment.ToString());
        
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; i++)
            {
                GameObject unitObject = units[unit.Id];
                CreateUnit(unit, unitObject, i);
            }
        }
    }

    private void CreateUnit(Unit unit, GameObject unitObject, int num)
    {
        Vector3 position = GetPosition(num, unit.Id); // TODO 위치로 바꿔야 함
        GameObject gameObject = CreateObject(unitObject, position);
        gameObject.AddComponent<UnitInstance>().Init(unit);
        // gameObject.AddComponent<UnitHealthBar>();
        gameObject.tag = unitTag;
    }

    private Vector3 GetPosition(int num, int row)
    {
        Vector3 localPosition = new Vector3(-1 + num, 0, row);
        Vector3 position = spawnPoint.position + localPosition;
        return position;
    }

    private GameObject CreateObject( GameObject unitObject, Vector3 position)
    {
        GameObject gameObject = Instantiate(unitObject, position, Quaternion.identity, UnitParent) as GameObject;
        return gameObject;
    }
}
