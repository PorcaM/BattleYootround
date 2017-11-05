using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum YootCount { Nak, Do, Gae, Gul, Yoot, Mo, BackDo = 1 };
    private GameObject[] horseObjects;

    void Start()
    {
        horseObjects = GameObject.FindGameObjectsWithTag("AllyHorse");
    }

    void ProceedTurn()
    {
        YootCount yootCount = ThrowYoot();
        if (yootCount == YootCount.Nak)
            return;
        else
            MoveHorse();
    }

    private YootCount ThrowYoot()
    {
        return YootCount.Do;
    }

    private void MoveHorse()
    {

    }
}
