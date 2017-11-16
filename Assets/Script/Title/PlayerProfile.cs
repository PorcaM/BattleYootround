using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour {
    public int level;
    public int exp;
    public int maxExp;

	// Use this for initialization
	void Start () {
        Init();
	}
    
    private void Init()
    {
        level = 1;
        exp = 0;
        maxExp = 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
