using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour {
    public GameObject fieldPref;
    private static List<GameObject> fields;

    public static List<GameObject> Fields
    {
        get
        {
            return fields;
        }

        set
        {
            fields = value;
        }
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        Fields = YootFieldFactory.CreateYootFields(fieldPref, transform);
    }

    public static YootField GetStartPoint()
    {
        return Fields[0].GetComponent<YootField>();
    }
}
