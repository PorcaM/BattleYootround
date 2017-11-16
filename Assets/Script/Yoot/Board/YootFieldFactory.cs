using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootFieldFactory : MonoBehaviour
{
    public BattleManager battleManager;
    public const int YootFieldCount = 30;
    public const int OutsideCount = 20;
    public const int VerticalCount = 5;
    public const int HorizonCount = 5;

    public GameObject fieldObj;
    public float radius;
    public Transform parent;

    public List<GameObject> CreateYootFields()
    {
        List<GameObject> fields = new List<GameObject>();
        for (int i = 0; i < YootFieldCount; i++)
        {
            GameObject yfObj = CreateYootField(i);
            fields.Add(yfObj);
        }
        return fields;
    }

    public GameObject CreateYootField(int i)
    {
        Vector3 pos = GetPosition(i);
        Quaternion quaternion = Quaternion.identity;
        GameObject yfObj = Instantiate(fieldObj, pos, quaternion, parent) as GameObject;
        yfObj.name = "YootField" + i;
        YootField field = yfObj.GetComponent<YootField>();
        field.id = i;
        field.milestone = HorseRoute.Type.Summer;
        field.battleManager = battleManager;
        return yfObj;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 pos;
        if (IsOutside(i))
            pos = GetOutsidePos(i);
        else if (IsVertical(i))
            pos = GetVerticalPos(i);
        else
            pos = GetHorizonPos(i);
        return pos;
    }

    private bool IsOutside(int index)
    {
        return index < OutsideCount;
    }

    private Vector3 GetOutsidePos(int index)
    {
        const float startAngle = 270.0f;
        const float intervalAngle = 18.0f;
        float angle = startAngle + (intervalAngle * index);
        float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
        float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        return new Vector3(x, 0, z);
    }

    private bool IsVertical(int index)
    {
        return index < (OutsideCount + VerticalCount);
    }

    private Vector3 GetVerticalPos(int index)
    {
        int innerIndex = index - OutsideCount;
        float interval = radius * 2 / (VerticalCount + 1);
        float x = radius - (innerIndex + 1) * interval;
        return new Vector3(x, 0, 0);
    }

    private Vector3 GetHorizonPos(int index)
    {
        int innerIndex = index - OutsideCount - VerticalCount;
        float interval = radius * 2 / (HorizonCount + 1);
        float z = radius - (innerIndex + 1) * interval;
        return new Vector3(0, 0, z);
    }
}
