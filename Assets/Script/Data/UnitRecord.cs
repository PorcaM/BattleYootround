using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRecord :MonoBehaviour
{
    public static Unit[] units;

    public static Unit[] Units
    {
        get
        {
            return units;
        }
    }

    public static void Init(Unit[] record)
    {
        units = record;
    }
}
