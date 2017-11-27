using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour {
    public Transform unitParent;
    public UnitUIFactory factory;
    public Deck deck;
    public List<UnitUI> uiList;

    void Start()
    {
        uiList = new List<UnitUI>();
    }

    public void Init(Deck deck)
    {
        this.deck = deck;
        uiList.Clear();
        foreach (Unit unit in deck.units)
            CreateUnitUI(unit);
    }

    private void CreateUnitUI(Unit unit)
    {
        UnitUI unitUI = factory.Create(unit, unitParent);
        unitUI.gameObject.AddComponent<EditableUnit>().Init(this, false);
        uiList.Add(unitUI);
    }

    public void Add(Unit unit)
    {
        deck.units.Add(unit);
        CreateUnitUI(unit);
    }

    public void Remove(UnitUI unitUI)
    {
        deck.units.Remove(unitUI.unit);
        uiList.Remove(unitUI);
        Destroy(unitUI.gameObject);
    }
}
