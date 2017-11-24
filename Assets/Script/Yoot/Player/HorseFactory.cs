using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseFactory : MonoBehaviour {
    public Horse horseModel;
    public Transform horseParent;

    public Horse CreateHorse(YootPlayer owner, int id)
    {
        Horse horse = Instantiate(horseModel, horseParent) as Horse;
        horse.Init(owner, id);
        return horse;
    }
}
