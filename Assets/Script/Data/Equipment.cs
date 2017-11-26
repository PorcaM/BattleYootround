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
        for (int i = 0; i < 4; ++i)
        {
            spellbook.spells.Add(SpellRecord.spells[list[i]]);
        }
        for (int i = 0; i < 5; ++i)
        {
            deck.units.Add(UnitRecord.units[list[i+4]]);
        }
    }

    public void TempInit()
    {
        ToastManager.Show("Equipment: temp init");
        deck = new Deck();
        spellbook = new Spellbook();
        List<Spell> tempS = new List<Spell>();
        for (int i = 0; i < 4; ++i)
        {
            Spell spell = SpellRecord.spells[i+1];
            tempS.Add(spell);
        }
        spellbook.spells = tempS;
        List<Unit> tempU = new List<Unit>();
        for (int i = 0; i < 5; ++i)
        {
            Unit unit = UnitRecord.units[i+1];
            tempU.Add(unit);
        }
        deck.units = tempU;
    }
}
