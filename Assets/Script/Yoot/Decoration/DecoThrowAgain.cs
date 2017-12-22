﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class DecoThrowAgain : MonoBehaviour {
    public void Show(float timerInterval)
    {
        DialogAlert dialog = DialogManager.CreateAlert();
        string titleText = "Throw Again";
        string bodyText = "Not enough height!!";
        dialog.Initialize(bodyText, null, "OK", titleText, MaterialIconHelper.GetRandomIcon(), null, null);
        dialog.dialogAnimator = new DialogAnimatorSlide(0.5f, DialogAnimatorSlide.SlideDirection.Right, DialogAnimatorSlide.SlideDirection.Left);
        dialog.Show();
        StartCoroutine(HideWindowAfterSeconds(dialog, timerInterval));
    }

    private IEnumerator HideWindowAfterSeconds(MaterialDialog dialog, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (dialog)
            dialog.Hide();
    }
}