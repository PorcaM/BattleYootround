using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseFactory : MonoBehaviour {
    public YootPlayer owner;
    public GameObject horseModel;
    public Transform horseParent;

    public GameObject CreateHorse()
    {
        GameObject horseObj = Instantiate(horseModel, horseParent) as GameObject;
        Horse horse = horseObj.GetComponent<Horse>();
        horse.owner = owner;
        horse.currentLocation = YootBoard.GetStartPoint();
        return horseObj;
    }
}
