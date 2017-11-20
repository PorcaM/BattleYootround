using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour {
    public float currentHealth;
    public float maxHealth;
    public Vector3 screenPos;
    public float percentage;

    public static Texture2D green;
    public static Texture2D red;

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            percentage = currentHealth / maxHealth;
        }
    }

    public static void Init()
    {
        green = new Texture2D(1, 1);
        green.SetPixel(0, 0, Color.green);
        green.wrapMode = TextureWrapMode.Repeat;
        green.Apply();

        red = new Texture2D(1, 1);
        red.SetPixel(0, 0, Color.red);
        red.wrapMode = TextureWrapMode.Repeat;
        red.Apply();
    }

    void OnGUI()
    {
        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float top = Screen.height - screenPos.y;
        float left = screenPos.x;
        float width = 50;
        float height = 5;
        if (percentage != 0.0f)
        {
            float x = left - width / 2;
            float y = top - 10;
            GUI.DrawTexture(new Rect(x, y, width, height), red);
            GUI.DrawTexture(new Rect(x, y, (width * percentage), height), green);
        }
    }
}
