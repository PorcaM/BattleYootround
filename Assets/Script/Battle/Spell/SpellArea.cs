using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellArea : MonoBehaviour {

    void Update()
    {
        UpdatePosition();
    }
    
    private void UpdatePosition()
    {
        float distance = Camera.main.transform.position.y;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        transform.position = rayPoint;
        transform.position = new Vector3(transform.position.x, .1f, transform.position.z);
    }
}