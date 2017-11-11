using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddForce : MonoBehaviour {
    public Rigidbody rigidbody;
    public float thrust;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            rigidbody.AddForce(new Vector3(0, 1, 0.2f) * thrust);
        if (Input.GetKeyDown("space"))
            rigidbody.AddForce(new Vector3(0, 0, 0.1f) * thrust);

    }
}
