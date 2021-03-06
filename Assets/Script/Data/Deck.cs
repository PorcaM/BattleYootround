﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MaterialUI;

public class Deck : MonoBehaviour{
    private const int deckSize = 5;
    public List<Unit> units = new List<Unit>(DeckSize);

    public string[] names;

    public static int DeckSize
    {
        get
        {
            return deckSize;
        }
    }

    public List<Unit> Units
    {
        get
        {
            return units;
        }

        set
        {
            units = value;
        }
    }

    public bool CheckIntegrity()
    {
        bool integrity = true;
        if (IsRedundancy())
        {
            integrity = false;
            DialogManager.ShowAlert("Deck redundancy", "Equip Integrity Error", MaterialIconHelper.GetIcon(MaterialIconEnum.DATA_USAGE));
        }
        if (!IsAppropriateSize())
        {
            integrity = false;
            DialogManager.ShowAlert("Deck size", "Equip Integrity Error", MaterialIconHelper.GetIcon(MaterialIconEnum.DATA_USAGE));
        }
        return integrity;
    }

    // TODO Configure which algorithm is better. 
    private bool IsRedundancy()
    {
        bool redundancy;
        // redundancy = Units.GroupBy(n => n).Any(c => c.Count() > 1);
        redundancy = units.Distinct().Count() != units.Count();
        return redundancy;
    }

    private bool IsAppropriateSize()
    {
        return Units.Count() == DeckSize;
    }

    public new string ToString()
    {
        string text = "Deck: ";
        foreach (Unit unit in units)
        {
            text += unit.UnitClass.ToString();
            text += ", ";
        }
        text += "\n";
        return text;
    }

    void Update()
    {
        names = new string[units.Count];
        for (int i = 0; i < units.Count; ++i)
            names[i] = units[i].UnitClass.ToString();
    }
}