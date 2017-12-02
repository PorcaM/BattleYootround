using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleYoot : MonoBehaviour {
    private Vector3 rot;
    private float minRot = -90.0f;
    private float maxRot = 90.0f;

	// Use this for initialization
	void Start () {
        transform.position += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
        rot = new Vector3(Random.Range(minRot, maxRot), Random.Range(minRot, maxRot), Random.Range(minRot, maxRot));
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rot * Time.deltaTime);
	}
}
