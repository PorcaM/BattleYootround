using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour {
    public UIActiveController uiActiveController;

    public void OnEnterBattle()
    {
        uiActiveController.SetUIsActive(true);
    }

    public void OnExitBattle()
    {
        uiActiveController.SetUIsActive(false);
    }
}
