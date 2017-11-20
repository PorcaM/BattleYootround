using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRecord : MonoBehaviour
{
    public static Spell[] spells;
    public static bool isInitialized = false;

    public static void Init(Spell[] record)
    {
        spells = record;
        isInitialized = true;
    }
}
