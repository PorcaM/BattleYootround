using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleYoot : MonoBehaviour {
    private Vector3 rot;
    private float minRot = -90.0f;
    private float maxRot = 90.0f;

	// Use this for initialization
	void Start () {
        Vector3 dest = transform.position + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
        gameObject.AddComponent<HorseAnimator>().Init(new Vector3(0, 0, 0), dest, 2.5f);
        rot = new Vector3(Random.Range(minRot, maxRot), Random.Range(minRot, maxRot), Random.Range(minRot, maxRot));
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rot * Time.deltaTime);
	}
}
