using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMinHeight : MonoBehaviour {
    public bool enough = false;
    public float minHeight = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > minHeight)
        {
            enough = true;
        }
	}
}
