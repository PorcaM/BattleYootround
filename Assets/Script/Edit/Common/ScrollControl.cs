using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour {
    private ScrollRect scrollRect;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void Next()
    {
        scrollRect.horizontalNormalizedPosition += .3f;
    }

    public void Back()
    {
        scrollRect.horizontalNormalizedPosition -= .3f;
    }
}
