using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUIInitializer : MonoBehaviour {
    public DeckUI deckUI;
    public UnitRecordUI unitRecordUI;

    public Deck deck;

    public void Init(Deck deck)
    {
        this.deck = deck;
        deckUI.Init(deck);
        unitRecordUI.Init();
    }
}
