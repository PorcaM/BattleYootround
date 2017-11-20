using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour
{
    public enum Route { Spring, Summer, Autumn, Winter };
    public GameObject fieldPref;
    public static List<GameObject> fieldObjs;
    public YootFieldFactory yootFieldFactory;
    public float radius = 4.0f;

    public static bool isReady = false;

    public void Init()
    {
        CreateFields();
        InitMilestone();
        RemoveDulication();
        isReady = true;
    }

    private void CreateFields()
    {
        yootFieldFactory.radius = radius;
        fieldObjs = yootFieldFactory.CreateYootFields();
    }

    private void InitMilestone()
    {
        fieldObjs[5].GetComponent<YootField>().milestone = HorseRoute.Type.Autumn;
        fieldObjs[10].GetComponent<YootField>().milestone = HorseRoute.Type.Spring;
        fieldObjs[22].GetComponent<YootField>().milestone = HorseRoute.Type.Winter;
    }

    private void RemoveDulication()
    {
        fieldObjs[27].SetActive(false);
    }    

    public static YootField GetStartPoint()
    {
        return fieldObjs[0].GetComponent<YootField>();
    }

    public static YootField GetDestination(Horse horse, YootGame.YootCount yootCount)
    {
        YootField source = horse.currentLocation;
        int[] route = HorseRoute.routes[(int)horse.routeType];
        int destIndex = HorseRoute.GetDestIndex(route, source.id, (int)yootCount);
        int destFieldID = route[destIndex];
        GameObject destinationObject = fieldObjs[destFieldID];
        return destinationObject.GetComponent<YootField>();
    }
}
