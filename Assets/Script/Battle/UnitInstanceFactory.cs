using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour {
    public Equipment equipment;
    public Transform spawnPoint;
    public Transform UnitParent;
    public const int instancePerUnit = 3;
    
    public GameObject[] units = new GameObject[7];

    public void CreateUnits(string tag)
    {
        equipment.TempInit();
        Debug.Log(equipment.ToString());
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; i++)
            {
                CreateUnit(unit, i, tag);
            }
        }
    }

    private void CreateUnit(Unit unit, int i, string tag)
    {
        Vector3 position = new Vector3(-1 + i, 0, unit.Id); // TODO 위치로 바꿔야 함
        position += spawnPoint.position;
        GameObject unitObject = units[unit.Id];
        GameObject unitInstance = Instantiate(unitObject, position, Quaternion.identity, UnitParent) as GameObject;
        unitInstance.AddComponent<UnitInstance>().Init(unit);
        // unitInstance.AddComponent<UnitHealthBar>();
        unitInstance.tag = tag;
    }
}
