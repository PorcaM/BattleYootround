using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spellbook : MonoBehaviour {
    private const int spellbookSize = 4;
    private List<Spell> spells = new List<Spell>(SpellbookSize);

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
}
