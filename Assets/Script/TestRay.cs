using UnityEngine;
using System.Collections;

public class TestRay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("hit");
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }
}