using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 1.5f;
    public bool isEnable = false;
    private Vector3 dragOrigin;

    void Update()
    {
        if (isEnable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;
            if (dragOrigin == Vector3.zero) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            transform.position += move;
            dragOrigin = Input.mousePosition;
        }
    }
}
