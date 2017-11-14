using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour {
    public GameObject fieldPref;
    public static List<GameObject> fieldObjects;

    public static List<GameObject> Fields
    {
        get
        {
            return fieldObjects;
        }

        set
        {
            fieldObjects = value;
        }
    }

    void Start()
    {
        YootFieldFactory.radius = 4.0f;
        Fields = YootFieldFactory.CreateYootFields(fieldPref, transform);
        YootBoard.Init();
    }

    public static void Init()
    {
        for (int i = 0; i < Fields.Count; ++i)
        {
            Fields[i].GetComponent<YootField>().Id = i;
        }
    }

    public static YootField GetStartPoint()
    {
        return Fields[0].GetComponent<YootField>();
    }

    public static YootField GetDestination(Horse horse, YootGame.YootCount yootCount)
    {
        YootField source = horse.currentLocation;
        GameObject destinationObject = Fields[GetDestinationId(source.Id, (int)yootCount)];
        return destinationObject.GetComponent<YootField>();
    }

    private static int GetDestinationId(int source, int yootCount)
    {
        int destination;
        destination = source + yootCount;
        if (destination > 19)
            destination = 0;
        return destination;
    }
}
