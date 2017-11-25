using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelManager : MonoBehaviour {
    public AccelerationEvent aEvent;

    public float max;
    public float min;

    // Update is called once per frame
    void Update()
    {
        float magnitude = Input.acceleration.magnitude;
        if (max < magnitude)
            max = magnitude;
        if (min > magnitude)
            min = magnitude;
    }

    public float MaxMagnitude()
    {
        float maxMagnitude = Mathf.Abs(min);
        if (maxMagnitude < max)
            maxMagnitude = max;
        return maxMagnitude;
    }
}
