using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator : MonoBehaviour {
    public GameObject[] targets = new GameObject[4];
    public float userPower;
    public float thrust;

    public void ForceTargets()
    {
        foreach (GameObject target in targets)
            ForceTarget(target);
    }

    private void ForceTarget(GameObject target)
    {
        Rigidbody rigidbody = target.GetComponent<Rigidbody>();
        ThrowUp(rigidbody);
        AddRotation(rigidbody);
    }

    private void ThrowUp(Rigidbody rigidbody)
    {
        rigidbody.AddForce(transform.up * thrust * userPower);
        Vector3 force= transform.up * thrust * userPower;
        Debug.Log("Throw force " + force);
    }

    private void AddRotation(Rigidbody rigidbody)
    {
        Vector3 force = transform.up * thrust * Random.Range(-1f, 1f) * userPower;
        rigidbody.AddTorque(transform.up * thrust * Random.Range(-1f, 1f) * userPower);
        Debug.Log(force);
        rigidbody.AddTorque(transform.right * thrust * Random.Range(-1f, 1f) * userPower);
        rigidbody.AddTorque(transform.forward * thrust * Random.Range(-1f, 1f) * userPower);
    }
}
