using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour {
    public UIActiveController uiActiveController;
    public Text battleResultText;

    public void OnEnterBattle()
    {
        uiActiveController.SetUIsActive(true);
    }

    public void OnExitBattle()
    {
        battleResultText.enabled = false;
        uiActiveController.SetUIsActive(false);
    }
}
