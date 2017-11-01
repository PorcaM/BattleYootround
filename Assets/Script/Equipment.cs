using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    public Deck deck;
    public Spellbook spellbook;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
