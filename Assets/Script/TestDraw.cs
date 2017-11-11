using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour {
    public GameObject trailPrefab;
    GameObject thisTrail;
    Vector3 startPos;
    Plane objPlane;

    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward, this.transform.position);
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            thisTrail = (GameObject)Instantiate(trailPrefab,
                                                    this.transform.position,
                                                    Quaternion.identity);

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                startPos = mRay.GetPoint(rayDistance);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                //Debug.Log(thisTrail);
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Ended");
            Debug.Log(thisTrail);
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                Destroy(thisTrail);
        }
        /*
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0))
        {
            thisTrail = (GameObject)Instantiate(trailPrefab,
                                                    this.transform.position,
                                                    Quaternion.identity);

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            { 
                startPos = mRay.GetPoint(rayDistance);
            }
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
             || Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                //Debug.Log(thisTrail);
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            || Input.GetMouseButtonUp(0))
        {
            Debug.Log("Ended");
            Debug.Log(thisTrail);
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                Destroy(thisTrail);
        }
        */
    }
}
