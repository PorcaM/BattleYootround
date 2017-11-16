using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRange : Range
{
    private Vector2 point;
    private Vector2 range;

    public Vector2 GetRange()
    {
        return range;
    }
    public void SetRange(Vector2 _range)
    {
        range = _range;
    }
    
    public RectRange(Vector2 range)
    {
        Init(range);
    }

    public void Init(Vector2 _range)
    {
        range = _range;
    }

    public override List<UnitInstance> SelectTarget()
    {
        return new List<UnitInstance>();
    }
}
