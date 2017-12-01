﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class EnterBattleDecorator : MonoBehaviour {
    private string titleText;
    private ImageData icon;

    public void ShowCountdown(int seconds)
    {
        titleText = "Battle Countdown";
        icon = MaterialIconHelper.GetIcon(MaterialIconEnum.HOURGLASS_EMPTY);
        StartCoroutine(ShowDialogs(seconds));
    }

    private IEnumerator ShowDialogs(int seconds)
    {
        for (int i = seconds; i > 0; --i)
        {
            ShowDialog(i);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void ShowDialog(int time)
    {
        Debug.Log(time);
        DialogProgress dialog = DialogManager.CreateProgressCircular();
        string bodyText = time.ToString() + " seconds left until the battle begins !!";
        dialog.Initialize(bodyText, titleText, icon);
        dialog.dialogAnimator = new DialogAnimatorSlide(1.0f, DialogAnimatorSlide.SlideDirection.Right, DialogAnimatorSlide.SlideDirection.Left);
        dialog.Show();
        StartCoroutine(HideWindowAfterSeconds(dialog, 1.0f));
    }

    private IEnumerator HideWindowAfterSeconds(MaterialDialog dialog, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (dialog)
            dialog.Hide();
    }
}