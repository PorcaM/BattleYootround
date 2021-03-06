﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public Animator animator;
    private Text damageText;

    public void Init()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
    }

    public void SetColor(string colorText)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString(colorText, out color);
        damageText.color = color;
    }

    public void SetText(string text)
    {
        damageText.text = text;
    }
}
