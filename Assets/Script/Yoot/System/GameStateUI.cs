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

    public void SetValue(TurnProcessor.ProcessState processState)
    {
        slider.value = (int)processState;
        popupText.text = processState.ToString();
    }

    public void SetColor(int player)
    {
        slider.GetComponent<MaterialSlider>().enabledColor = GetColor(player);
    }
    
    private Color GetColor(int player)
    {
        Color color;
        ColorUtility.TryParseHtmlString(GetColorString(player), out color);
        return color;
    }

    private string GetColorString(int player)
    {
        string colorString = enemyColorString;
        if (player == 0)
            colorString = allyColorString;
        return colorString;
    }
}
