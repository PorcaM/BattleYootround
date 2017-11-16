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

    public float radius = 4.0f;

    public List<GameObject> CreateYootFields(GameObject fieldPref, Transform parent)
    {
        List<GameObject> fields = new List<GameObject>();
        for (int i = 0; i < YootFieldCount; i++)
        {
            Vector3 pos;
            if (IsOutside(i))
                pos = GetOutsidePos(i);
            else if (IsVertical(i))
                pos = GetVerticalPos(i);
            else
                pos = GetHorizonPos(i);
            GameObject newObject = Instantiate(fieldPref, pos, Quaternion.identity, parent) as GameObject;
            newObject.GetComponent<YootField>().Id = i;
            newObject.name = "YootField" + i;
            newObject.GetComponent<YootField>().battleManager = battleManager;
            fields.Add(newObject);
        }
        return fields;
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
