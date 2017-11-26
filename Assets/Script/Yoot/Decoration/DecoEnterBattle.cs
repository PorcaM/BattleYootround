using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class DecoEnterBattle : MonoBehaviour {
    public System.Action action;

    public void OnEnterBattle(float timerInterval)
    {
        DialogAlert dialog = DialogManager.CreateAlert();
        string titleText = "Battle";
        string bodyText = "Battle is occured!!";
        dialog.Initialize(bodyText, action, "OK", titleText, MaterialIconHelper.GetRandomIcon(), action, null);
        dialog.dialogAnimator = new DialogAnimatorSlide(0.5f, DialogAnimatorSlide.SlideDirection.Right, DialogAnimatorSlide.SlideDirection.Left);
        dialog.Show();
        StartCoroutine(HideWindowAfterSeconds(dialog, timerInterval));
    }

    private IEnumerator HideWindowAfterSeconds(MaterialDialog dialog, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (dialog)
        {
            dialog.Hide();
            action.Invoke();
        }
    }
}
