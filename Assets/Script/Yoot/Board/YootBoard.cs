using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour
{
    public GameObject fieldPref;
    public static List<GameObject> fieldObjects;
    public YootFieldFactory yootFieldFactory;

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
        yootFieldFactory.radius = 4.0f;
        Fields = yootFieldFactory.CreateYootFields(fieldPref, transform);
        Init();
    }

    public static void Init()
    {
        for (int i = 0; i < Fields.Count; ++i)
        {
            YootField field = Fields[i].GetComponent<YootField>();
            field.Id = i;
            if (i == 5) field.milestone = Horse.RunningRoute.Horizon;
            if (i == 10) field.milestone = Horse.RunningRoute.Vertical;
            if (i == 22) field.milestone = Horse.RunningRoute.Shortest;
            if (i == 27) Fields[i].SetActive(false);
        }
    }

    public static YootField GetStartPoint()
    {
        return Fields[0].GetComponent<YootField>();
    }

    public static YootField GetDestination(Horse horse, YootGame.YootCount yootCount)
    {
        YootField source = horse.currentLocation;
        int[] route = HorseRoute.routes[(int)horse.CurrentRoute];
        int destIndex = HorseRoute.GetDestIndex(route, source.Id, (int)yootCount);
        int destFieldID = route[destIndex];
        GameObject destinationObject = Fields[destFieldID];
        return destinationObject.GetComponent<YootField>();
    }
}
