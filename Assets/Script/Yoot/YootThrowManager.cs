using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YootThrowManager : MonoBehaviour {
    public static YootGame.YootCount Throw()
    {
        return SimpleRandom();
    }

    private static YootGame.YootCount SimpleRandom()
    {
        int count = Enum.GetValues(typeof(YootGame.YootCount)).Length;
        return (YootGame.YootCount) UnityEngine.Random.Range(0, count);
    }
}
