using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range {
    public virtual List<UnitInstance> SelectTarget(Vector3 pos)
    {
        return new List<UnitInstance>();
    }
}
