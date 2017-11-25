using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseAnimator : MonoBehaviour
{
    public Vector3 srcPos;
    public Vector3 dstPos;
    public float speed = 5.0F;
    public float timeLimit;
    private float startTime;

    public void Init(Vector3 srcPos, Vector3 dstPos, float timeLimit)
    {
        this.srcPos = srcPos;
        this.dstPos = dstPos;
        this.timeLimit = timeLimit;
        startTime = Time.time;
    }

    void Update()
    {
        float leftTime = 1 - (Time.time - startTime) / timeLimit;
        float fraction = 1 - (Mathf.Pow(leftTime, 10));
        transform.position = Vector3.Lerp(srcPos, dstPos, fraction);
        if (transform.position == dstPos)
            Destroy(this);
    }
}
