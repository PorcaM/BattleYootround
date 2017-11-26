using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchUIManager : MonoBehaviour {
    public Equipment equipment;
    public SpellbookUIController spellbookUIController;
    public DeckUIController deckUIController;

	// Use this for initialization
	void Start () {
        equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        Debug.Log(equipment.ToString());
        spellbookUIController.spellbook = equipment.spellbook;
        spellbookUIController.Apply();
        deckUIController.deck = equipment.deck;
        deckUIController.Apply();
    }
}
