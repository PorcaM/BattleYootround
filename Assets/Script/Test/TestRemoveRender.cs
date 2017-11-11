using UnityEngine;
using System.Collections;

public class TestRemoveRender : MonoBehaviour
{
    public GameObject[] Trails;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clear()
    {
        Trails = GameObject.FindGameObjectsWithTag("Drawing");
        foreach(GameObject trail in Trails)
        {
            Destroy(trail);
        }
    }

}
