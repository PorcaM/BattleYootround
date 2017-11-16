using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private Vector3 backupPosition;
    private float distance;
    public List<GameObject> destinations;

    void Start()
    {
        destinations = new List<GameObject>();
    }

    void OnMouseDown()
    {
        GetComponent<Horse>().Selected();
        distance = Camera.main.transform.position.y;
        backupPosition = transform.position;
        AddDestinations();
    }

    private void AddDestinations()
    {
        GameObject[] fields = GameObject.FindGameObjectsWithTag("YootField");
        foreach (GameObject field in fields)
        {
            if (field.GetComponent<YootField>().DestFlag)
                destinations.Add(field);
        }
    }

    void OnMouseUp()
    {
        GameObject destination = IsOnDestination();
        if (destination != null)
        {
            YootField field = destination.GetComponent<YootField>();
            field.Selected();
        }
        else
        {
            transform.position = backupPosition;
        }
        destinations.Clear();
    }

    private GameObject IsOnDestination()
    {
        foreach(GameObject field in destinations)
        {
            if(transform.position == field.transform.position)
            {
                return field;
            }
        }
        return null;
    }

    void OnMouseDrag()
    {
        Move();
        foreach(GameObject field in destinations)
        {
            if (IsInSnapRange(field))
            {
                SnapTo(field);
            }
        }
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        transform.position = rayPoint;
    }

    private bool IsInSnapRange(GameObject field)
    {
        float distance = Vector3.Distance(field.transform.position, transform.position);
        const float snapRange = 0.4f;
        return distance < snapRange;
    }

    private void SnapTo(GameObject field)
    {
        transform.position = field.transform.position;
    }
}
