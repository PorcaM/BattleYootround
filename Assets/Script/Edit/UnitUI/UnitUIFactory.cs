using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUIFactory : MonoBehaviour {
    public UnitUI original;

    public UnitUI Create(Unit unit, Transform parent)
    {
        UnitUI unitUI = Instantiate(original, parent);
        unitUI.Init(unit);
        unitUI.name = "SpellUI:" + unit.UnitClass.ToString();
        return unitUI;
    }
}
