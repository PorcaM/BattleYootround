using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour {
    public List<GameObject> battleObjs;
    public List<GameObject> yootObjs;

    public void SetActiveAll(bool value)
    {
        SetActiveBattle(value);
        SetActiveYoot(value);
    }

    private void SetActiveBattle(bool value)
    {

    }

    private void SetActiveYoot(bool value)
    {

    }
}
