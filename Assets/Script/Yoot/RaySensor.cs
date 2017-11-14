using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour {
	void Update ()
    {
        if (IsInputOccured())
        {
            RaycastHit hit;
            if (IsInputHit(out hit))
            {
                if (hit.collider != null)
                {
                    Horse horse = hit.collider.GetComponentInParent<Horse>();
                    if (horse)
                    {
                        horse.Selected();
                    }
                    YootField field = hit.collider.GetComponentInParent<YootField>();
                    if (field)
                    {
                        field.Selected();
                    }
                }
            }
        }
    }

    private static bool IsInputOccured()
    {
        bool flag = false;
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                flag = true;
                break;
            }
        }
        if (Input.GetMouseButtonDown(0))
            flag = true;
        return flag;
    }

    private static bool IsInputHit(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}
