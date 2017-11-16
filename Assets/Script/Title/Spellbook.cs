using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spellbook : MonoBehaviour{
    private const int spellbookSize = 4;
    public List<Spell> spells = new List<Spell>(SpellbookSize);

    #region Properties
    public static int SpellbookSize
    {
        get
        {
            return spellbookSize;
        }
    }

    public List<Spell> Spells
    {
        get
        {
            return spells;
        }

        set
        {
            spells = value;
        }
    }
#endregion

    public bool CheckIntegrity()
    {
        bool integrity = true;
        if (IsRedundancy())
            integrity = false;
        if (!IsAppropriateSize())
            integrity = false;
        return integrity;
    }

    // TODO Configure which algorithm is better. 
    private bool IsRedundancy()
    {
        bool redundancy;
        redundancy = Spells.GroupBy(n => n).Any(c => c.Count() > 1);
        // redundancy = units.Distinct().Count() != units.Count();
        return redundancy;
    }

    private bool IsAppropriateSize()
    {
        return Spells.Count() == SpellbookSize;
    }

    public new string ToString()
    {
        string text = "Spellbook: ";
        foreach (Spell spell in spells)
        {
            text += spell.SpellName;
            text += ", ";
        }
        text += "\n";
        return text;
    }
}
