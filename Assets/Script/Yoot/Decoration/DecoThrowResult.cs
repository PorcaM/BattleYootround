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
}
