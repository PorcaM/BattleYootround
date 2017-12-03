using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoubleClickListener : MonoBehaviour
{
    public Action action;
    public bool isChecking = false;

    private bool isPrev;
    private float lastTime;
    private const float timeInterval = 0.25f;

    public void Init(Action action)
    {
        this.action = action;
        isChecking = false;
    }

    public void Setup()
    {
        isChecking = true;
        isPrev = false;
    }

    public void Cleanup()
    {
        isChecking = false;
    }

    void Update()
    {
        if (isChecking)
            if (IsInput())
                if (!isPrev)
                    OnFirst();
                else
                    if (Time.time - lastTime < timeInterval)
                    OnSecond();
                else
                    OnFirst();
    }

    private bool IsInput()
    {
        bool input = false;
        if (Input.GetMouseButtonDown(0))
            input = true;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            input = true;
        return input;
    }

    private void OnFirst()
    {
        isPrev = true;
        lastTime = Time.time;
    }

    private void OnSecond()
    {
        isPrev = false;
        action.Invoke();
    }
}
