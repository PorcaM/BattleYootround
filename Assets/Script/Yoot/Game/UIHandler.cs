using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    public GameObject myUI;
    public List<GameObject> others;

    void Start()
    {
        others = new List<GameObject>();
    }

    public void SetUIActive(bool flag)
    {
        myUI.SetActive(flag);
        foreach (GameObject ui in others)
            myUI.SetActive(!flag);
    }
}
