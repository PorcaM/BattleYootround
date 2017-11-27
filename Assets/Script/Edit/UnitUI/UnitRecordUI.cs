using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRecordUI : MonoBehaviour {
    public Transform unitParent;
    public UnitUIFactory factory;
    public DeckUI deckUI;

    public void Init()
    {
        foreach (Unit unit in UnitRecord.units)
        {
            if (unit.UnitClass.ToString() != "Basic")
            {
                UnitUI unitUI = factory.Create(unit, unitParent);
                unitUI.gameObject.AddComponent<EditableUnit>().Init(deckUI, true);
            }
        }
    }
}
