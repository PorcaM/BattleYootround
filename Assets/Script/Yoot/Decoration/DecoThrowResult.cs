using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class DecoThrowResult : MonoBehaviour {
    public static void ShowResult(YootGame.YootCount result)
    {
        string content = "Yoot throw result:\n" + result.ToString();
        ToastManager.Show(content);
    }

    public void Show(YootGame.YootCount result, float timerInterval)
    {
        DialogAlert dialog = DialogManager.CreateAlert();
        string titleText = "Throw";
        string bodyText = "Throw result is " + result.ToString() + "!!";
        dialog.Initialize(bodyText, null, "OK", titleText, MaterialIconHelper.GetRandomIcon(), null, null);
        dialog.dialogAnimator = new DialogAnimatorSlide(0.5f, DialogAnimatorSlide.SlideDirection.Right, DialogAnimatorSlide.SlideDirection.Left);
        dialog.Show();
        StartCoroutine(HideWindowAfterSeconds(dialog, timerInterval));
    }

    private IEnumerator HideWindowAfterSeconds(MaterialDialog dialog, float duration)
    {
        yield return new WaitForSeconds(duration);
        dialog.Hide();
    }
}
