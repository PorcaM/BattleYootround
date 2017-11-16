using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseFactory : MonoBehaviour {
    public GameObject horseModel;

    public GameObject CreateHorse()
    {
        GameObject gameObject = Instantiate(horseModel) as GameObject;
        return gameObject;
    }
}
