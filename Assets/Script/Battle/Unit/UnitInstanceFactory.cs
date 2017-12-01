using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour
{
    public string unitTag;
    public Equipment equipment;
    public Transform LookatPoint;
    public Transform UnitParent;
    public UnitModels unitModels;
    public float spanwPosZ;
    public Transform center;

    public const int instancePerUnit = 3;

    public GameObject modellessUnit;

    public void CreateUnits()
    {
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; ++i)
                CreateUnit(unit, i);
        }
    }

    private void CreateUnit(Unit unit, int num)
    {
        GameObject unitObj = Instantiate(modellessUnit, UnitParent);
        GameObject model = CreateModel(unit, unitObj.transform);
        unitObj.name = unit.UnitClass.ToString() + num;
        unitObj.tag = unitTag;
        unitObj.GetComponent<UnitInstance>().Init(unit);
        unitObj.GetComponent<UnitAnimation>().Init(model);
        unitObj.transform.position = GetPosition(num, unit.position);
        unitObj.transform.LookAt(center);
    }

    private GameObject CreateModel(Unit unit, Transform parent)
    {
        const string path = "UnitImages/Models/";
        string modelName = unit.UnitClass.ToString().ToLower();
        GameObject model = Instantiate(Resources.Load(path + modelName, typeof(GameObject)), parent) as GameObject;
        return model;
    }

    private Vector3 GetPosition(int num, int row)
    {
        const float xInterval = .5f;
        const float zInterval = .5f;
        float dir = spanwPosZ / Mathf.Abs(spanwPosZ);
        float x = (num - 1) * xInterval + Random.Range(-.1f, .1f);
        float z = row * dir * zInterval + Random.Range(-.1f, .1f);
        Vector3 localPosition = new Vector3(x, 0, z);
        localPosition.z += spanwPosZ;
        Vector3 position = localPosition + center.position;
        return position;
    }
}
