using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPlayerProfile : MonoBehaviour {
    public PlayerProfile playerProfile;
    private Text textObject;

	// Use this for initialization
	void Start () {
        textObject = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
        textObject.text = "PlayerProfile\n" + "Level: " + playerProfile.level;
	}
}
