using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class DeckUIController : MonoBehaviour {
    public Deck deck;
    public Transform deckUI;

    public void Apply()
    {

        for (int i = 0; i < deck.units.Count; ++i)
        {
            Unit unit = deck.units[i];
            MaterialButton button = deckUI.GetChild(i).GetChild(1).GetComponent<MaterialButton>();
            button.textText = unit.UnitClass.ToString();
        }
    }
}
