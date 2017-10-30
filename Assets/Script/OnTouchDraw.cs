using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDraw : MonoBehaviour {
    public Texture2D sourceTex;
    public Rect sourceRect;

    private Touch tempTouchs;
    private Vector3 touchedPos;
    private bool touchOn;
    private Color[] blacks;
    private Color[] whites;


    void Start()
    {
        blacks = new Color[10 * 10];
        for(int i=0; i< blacks.Length; i++)
        {
            blacks[i] = Color.black;
        }

        whites = new Color[2000 * 2000];
        for(int i=0; i<whites.Length; i++)
        {
            whites[i] = Color.white;
        }

        sourceTex.SetPixels(whites);
        sourceTex.Apply();
    }

    private void Update()
    {
        touchOn = false;
        if (Input.GetMouseButton(0))
        {
            int x = Mathf.FloorToInt(Input.mousePosition.x);
            int y = Mathf.FloorToInt(Input.mousePosition.y);
            Debug.Log(x);
            Debug.Log(y);
            
            Vector3 pos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));


            x -= 5;
            y -= 5;

            sourceTex.SetPixels(x, y, 10, 10, blacks);
            sourceTex.Apply();
        }
        //touchOn = false;
        //if (Input.touchCount > 0)
        //{
        //    for(int i=0; i<Input.touchCount; i++)
        //    {
        //        tempTouchs = Input.GetTouch(i);
        //        if(tempTouchs.phase == TouchPhase.Began)
        //        {
        //            touchedPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);
        //            touchOn = true;

        //            Debug.Log(touchedPos);

        //            break;
        //        }
        //    }
        //}
    }

}
