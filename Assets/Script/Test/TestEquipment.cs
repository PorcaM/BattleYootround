using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEquipment : MonoBehaviour {
    public Spellbook spellbook;
    public Deck deck;
    public Equipment equipment;

	// Use this for initialization
	void Start () {
        equipment.TempInit();
        Debug.Log(equipment.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
