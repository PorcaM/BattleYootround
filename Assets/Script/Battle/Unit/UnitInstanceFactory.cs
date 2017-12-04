using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstanceFactory : MonoBehaviour
{
    public string unitTag;
    public Equipment equipment;
    public Transform UnitParent;
    public float spanwPosZ;
    public Transform center;

    public const int instancePerUnit = 3;

    public GameObject modellessUnit;

    // for network
    BYMessage.UnitPositionMessage msg = new BYMessage.UnitPositionMessage();
    int row_i = 0;

    public void CreateUnits()
    {
        foreach (Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; ++i)
                CreateUnit(unit, i);
        }
    }

    // 서버에서 온 pos로 CreateUnits(int[])함수에서 초기화
    public void CreateUnits(Vector3[] pos)
    {
        foreach(Unit unit in equipment.deck.Units)
        {
            for (int i = 0; i < instancePerUnit; ++i)
                CreateUnit(unit, i, msg.ally_pos[i]);
        }
    }

    private void CreateUnit(Unit unit, int num, Vector3 pos = default(Vector3))
    {
        GameObject unitObj = Instantiate(modellessUnit, UnitParent);
        GameObject model = CreateModel(unit, unitObj.transform);
        unitObj.name = unit.UnitClass.ToString() + num;
        unitObj.tag = unitTag;
        unitObj.GetComponent<UnitInstance>().Init(unit);
        unitObj.GetComponent<UnitAnimation>().Init(model);
        if (!YootGame.isNetwork)
            unitObj.transform.position = GetPosition(num, unit.position);
        else
            unitObj.transform.position = pos;
        unitObj.transform.LookAt(center);
    }

    private GameObject CreateModel(Unit unit, Transform parent)
    {
        const string path = "UnitImages/Models/";
        string modelName = unit.UnitClass.ToString().ToLower();
        GameObject model = Instantiate(Resources.Load(path + modelName, typeof(GameObject)), parent) as GameObject;
        return model;
    }

    public Vector3 GetPosition(int num, int row)
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
    public Vector3 GetPosition(int num, int row, int check)
    {
        const float xInterval = .5f;
        const float zInterval = .5f;
        float dir = spanwPosZ / Mathf.Abs(spanwPosZ);
        float x = (num - 1) * xInterval + Random.Range(-.1f, .1f);
        float z = row * dir * zInterval + Random.Range(-.1f, .1f);
        Vector3 localPosition = new Vector3(x, 0, z);
        localPosition.z += spanwPosZ;
        Vector3 position = localPosition + GameObject.Find("center").transform.position;
        return position;
    }
}
