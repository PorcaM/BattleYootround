using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnitHealthBar : MonoBehaviour {
    public float currentHealth;
    public float maxHealth;
    public Vector3 screenPos;
    public float percentage;

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

    void OnGUI()
    {
        //Gets coordinate our object on screen
        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float top = Screen.height - screenPos.y;
        float left = screenPos.x;
        float width = 50;
        float height = 5;
        if (percentage != 0.0f)
        {
            EditorGUI.DrawRect(new Rect(left, top, width, height), Color.red);
            EditorGUI.DrawRect(new Rect(left, top, (width * percentage), height), Color.green);
        }
    }
}
