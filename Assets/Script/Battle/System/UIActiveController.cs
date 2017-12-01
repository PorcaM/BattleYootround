using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveController : MonoBehaviour {
    public List<GameObject> battleUIList;
    public List<GameObject> yootUIList;

    public void SetUIsActive(bool value)
    {
        foreach (GameObject obj in battleUIList)
            obj.SetActive(value);
        foreach (GameObject obj in yootUIList)
            obj.SetActive(!value);
    }
}
