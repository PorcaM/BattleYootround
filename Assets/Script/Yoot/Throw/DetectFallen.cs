using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFallen : MonoBehaviour {
    public bool isFallen;
    public const float detectLine = -5.0f;

    private void FixedUpdate()
    {
        Detect();
    }

    private void Detect()
    {
        if (transform.position.y < detectLine)
            isFallen = true;
        else
            isFallen = false;
    }
}
