using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    public GameObject myUI;
    public List<GameObject> others;

    public void SetUIActive(bool flag)
    {
        Debug.Log("Hello" + flag);
        myUI.SetActive(flag);
        foreach (GameObject ui in others)
            ui.SetActive(!flag);
    }
}
