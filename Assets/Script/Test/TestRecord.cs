using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRecord : MonoBehaviour {
    public Spell[] spells;
    public Unit[] units;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Test()
    {
        Debug.Log("hello");
    }

    public void GetRecord()
    {
        spells = SpellRecord.Spells;
        units = UnitRecord.Units;
    }
}
