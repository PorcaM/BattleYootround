using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRange : Range
{
    private Vector2 point;
    private Vector2 range;

    public override List<UnitInstance> SelectTarget()
    {
        return new List<UnitInstance>();
    }
}
