using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDownside : MonoBehaviour {
    public bool isDownside;
    private const float dotThreshold = .5f;

    private void FixedUpdate()
    {
        Detect();
    }

    private void Detect()
    {
        if (Vector3.Dot(transform.up, Vector3.down) >= dotThreshold)
            isDownside = true;
        else
            isDownside = false;
    }
}
