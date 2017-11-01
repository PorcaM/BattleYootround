using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour {
    private const int deckSize = 5;
    private List<Unit> units = new List<Unit>(DeckSize);

    public static int DeckSize
    {
        get
        {
            return deckSize;
        }
    }

    public bool CheckIntegrity()
    {
        bool integrity = true;
        if (IsRedundancy())
            integrity = false;
        return integrity;
    }

    // TODO Configure which algorithm is better. 
    private bool IsRedundancy()
    {
        bool redundancy;
        redundancy = units.GroupBy(unit => unit).Any(c => c.Count() > 1);
        // redundancy = units.Distinct().Count() != units.Count();
        return redundancy;
    }
}