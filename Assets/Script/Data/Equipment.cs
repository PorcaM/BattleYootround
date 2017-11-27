using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class Equipment : MonoBehaviour
{
    public Spellbook spellbook;
    public Deck deck;

    public new string ToString()
    {
        string text = "Equipment: \n";
        text += deck.ToString() + spellbook.ToString();
        return text;
    }

    public void Init(List<int> list)
    {
        spellbook.spells.Clear();
        deck.units.Clear();
        for (int i = 0; i < 4; ++i)
        {
            spellbook.spells.Add(SpellRecord.spells[list[i]]);
        }
        for (int i = 0; i < 5; ++i)
        {
            deck.units.Add(UnitRecord.units[list[i+4]]);
        }
    }

    public void Init(Equipment origin)
    {
        spellbook.spells.Clear();
        deck.units.Clear();
        foreach(Spell spell in origin.spellbook.spells)
        {
            spellbook.spells.Add(spell);
        }
        foreach(Unit unit in origin.deck.units)
        {
            deck.units.Add(unit);
        }
    }

    public bool IsIntegrity()
    {
        return spellbook.CheckIntegrity() && deck.CheckIntegrity();
    }
}
