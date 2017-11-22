using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRange : Range
{
    private Vector2 point;
    public float radius;

    public float Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public CircleRange(float radius)
    {
        Init(radius);
    }

    public void Init(float radius)
    {
        Radius = radius;
    }

    public override List<UnitInstance> SelectTarget()
    {
        return new List<UnitInstance>();
    }
}
