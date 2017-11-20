using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRecord :MonoBehaviour
{
    public static Unit[] units;
    public static bool isInitialized = false;

    public static void Init(Unit[] record)
    {
        units = record;
        isInitialized = true;
    }
}
