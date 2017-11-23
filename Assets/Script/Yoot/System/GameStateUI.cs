using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class GameStateUI : MonoBehaviour {
    public Text popupText;
    public Slider slider;

    const string allyColorString = "#4CAF50FF";
    const string enemyColorString = "#E91E63FF";

    public void UpdateUI(TurnManager.ProcessState processState)
    {
        slider.value = (int)processState;
        popupText.text = processState.ToString();
    }

    public void UpdateColor(int playerID)
    {
        string colorString = enemyColorString;
        if (playerID == 0)
            colorString = allyColorString;
        Color color;
        ColorUtility.TryParseHtmlString(colorString, out color);
        Debug.Log(color);
        slider.GetComponent<MaterialSlider>().enabledColor = color;
    }
}
