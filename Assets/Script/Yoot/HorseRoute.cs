using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HorseRoute
{
    public static List<int[]> routes;

    public static void Init()
    {
        routes = new List<int[]>();
        int[] outside = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        int[] horizon = { 0, 1, 2, 3, 4, 5, 20, 21, 22, 23, 24, 15, 16, 17, 18, 19 };
        int[] vertical = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 25, 26, 22, 28, 29 };
        int[] shortest = { 0, 1, 2, 3, 4, 5, 20, 21, 22, 28, 29 };
        routes.Add(outside);
        routes.Add(horizon);
        routes.Add(vertical);
        routes.Add(shortest);
    }

    public static int GetDestIndex(int[] route, int srcID, int amount)
    {
        int goal = System.Array.IndexOf(route, srcID) + amount;
        if (goal < 0) goal = goal += route.Length;
        if (goal >= route.Length) goal = 0;
        return goal;
    }
}
