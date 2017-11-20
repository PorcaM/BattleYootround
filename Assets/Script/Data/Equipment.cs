using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Deck deck;
    public Spellbook spellbook;

    public new string ToString()
    {
        string text = "Equipment: \n";
        text += deck.ToString() + spellbook.ToString();
        return text;
    }

    public void TempInit()
    {
        deck = new Deck();
        spellbook = new Spellbook();
        List<Spell> tempS = new List<Spell>();
        for (int i = 0; i < 4; ++i)
        {
            Spell spell = SpellRecord.Spells[i];
            tempS.Add(spell);
        }
        spellbook.spells = tempS;
        List<Unit> tempU = new List<Unit>();
        for (int i = 0; i < 5; ++i)
        {
            Unit unit = UnitRecord.Units[i];
            tempU.Add(unit);
        }
        deck.units = tempU;
    }
}
