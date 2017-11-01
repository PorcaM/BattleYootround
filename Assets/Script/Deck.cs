using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < DeckSize; i++)
        {
            units.Contains(units[i]);
        }
        return true;
    }
}