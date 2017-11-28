using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator : MonoBehaviour {
    public GameObject[] targets = new GameObject[4];
    public float userPower;
    public float thrust;

    private float force;
    private List<Vector3> torques = new List<Vector3>();

    public void ForceTargets()
    {
        torques.Clear();
        force = thrust * userPower;
        foreach (GameObject target in targets)
            ForceTarget(target);
        LogPower();
    }

    private void LogPower()
    {
        Debug.Log(force);
        foreach (Vector3 torque in torques)
            Debug.Log(torque);
    }

    private void ForceTarget(GameObject target)
    {
        Rigidbody rigidbody = target.GetComponent<Rigidbody>();
        ThrowUp(rigidbody);
        AddRotation(rigidbody);
    }

    private void ThrowUp(Rigidbody rigidbody)
    {
        rigidbody.AddForce(Vector3.up * force);
    }

    private void AddRotation(Rigidbody rigidbody)
    {
        Vector3 torque = CreateTorque();
        rigidbody.AddTorque(torque * force);
    }

    private Vector3 CreateTorque()
    {
        Vector3 torque = Vector3.up * GetRandom() +
            Vector3.right * GetRandom() +
            Vector3.forward * GetRandom();
        torques.Add(torque);
        return torque;
    }

    private float GetRandom()
    {
        return Random.Range(-1f, 1f);
    }
}
