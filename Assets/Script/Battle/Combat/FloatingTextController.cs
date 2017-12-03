using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {
    private static FloatingText popupText;
    private static Transform parent;
    private static float randomMin = -1.5f;
    private static float randomMax = 1.5f;
    public static bool isWorking = true;

    public static void Init(FloatingText popupText, Transform parent)
    {
        FloatingTextController.popupText = popupText;
        FloatingTextController.parent = parent;
    }

    public static void CreateFloatingText(string text, Transform location, string colorText = "")
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(parent, false);
        Vector2 randomPosition = new Vector2(screenPosition.x + GetRandom(), screenPosition.y + GetRandom());
        instance.transform.position = randomPosition;
        instance.Init();
        instance.SetText(text);
        if (colorText != "")
            instance.SetColor(colorText);
    }

    private static float GetRandom()
    {
        return Random.Range(randomMin, randomMax);
    }
}
