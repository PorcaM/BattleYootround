using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRecord :MonoBehaviour{
    public static Spell[] spells;

    public static Spell[] Spells
    {
        get
        {
            return spells;
        }
    }

    public static void Init(Spell[] record)
    {
        spells = record;
    }
}
