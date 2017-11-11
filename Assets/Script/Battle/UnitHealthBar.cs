using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour {
    public Texture2D healthBar;
    public double currentHealth;
    public double maxHealth;
    public Vector3 screenPos;
    public float percentage;

    public double MaxHealth
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

    public double CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    void OnGUI()
    {
        //Gets coordinate our object on screen
         screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float top = Screen.height - screenPos.y;
        float left = screenPos.x;
        percentage = (float)(CurrentHealth / MaxHealth);
        GUI.DrawTexture(new Rect(left, top, (50 * percentage), 5), healthBar);
    }
}
