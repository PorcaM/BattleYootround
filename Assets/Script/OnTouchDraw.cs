using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDraw : MonoBehaviour {

    public Texture2D heightmap;
    public Vector3 size = new Vector3(100, 10, 100);

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        Vector3 pos = transform.position;
        pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        Debug.Log(pos);
        Debug.Log(heightmap.GetPixels());
    }
}
