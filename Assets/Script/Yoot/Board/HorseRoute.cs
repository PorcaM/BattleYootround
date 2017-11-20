using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HorseRoute
{
    public enum Type { Summer, Autumn, Spring, Winter };
    public static List<int[]> routes;

    public static void Init()
    {
        routes = new List<int[]>();
        int[] summer = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        int[] autumn = { 0, 1, 2, 3, 4, 5, 20, 21, 22, 23, 24, 15, 16, 17, 18, 19 };
        int[] spring = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 25, 26, 22, 28, 29 };
        int[] winter = { 0, 1, 2, 3, 4, 5, 20, 21, 22, 28, 29 };
        routes.Add(summer);
        routes.Add(autumn);
        routes.Add(spring);
        routes.Add(winter);
    }

    public static int GetDestIndex(int[] route, int srcID, int amount)
    {
        int goal = System.Array.IndexOf(route, srcID) + amount;
        if (goal < 0) goal += route.Length;
        if (goal >= route.Length) goal = 0;
        return goal;
    }
}
