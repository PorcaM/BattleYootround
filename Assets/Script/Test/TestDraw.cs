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
        // 터치 시작
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0))
        {
            thisTrail = (GameObject)Instantiate(trailPrefab,
                                                    this.transform.position,
                                                    Quaternion.identity);

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                startPos = mRay.GetPoint(rayDistance);
        }
        // 터치 시작 후 움직임
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
             || Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
        }
        // 터치 종료
        // 점을 찍고싶어할수도 있어서 일단 주석처리함
        /*
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            || Input.GetMouseButtonUp(0))
        {
            // 너무 적은 위치를 움직였을 경우 무시 (삭제)
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                Destroy(thisTrail);
        }
        */
        
    }
}
