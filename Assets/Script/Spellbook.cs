using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}
