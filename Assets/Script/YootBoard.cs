using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour {
    public GameObject fieldPref;
    public const int YootFieldCount = 29;
    private static List<YootField> fields;

    public static List<YootField> Fields
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
        Fields = new List<YootField>();
        for (int i = 0; i < YootFieldCount; i++)
        {
            float x = i;
            float y = 0;
            Vector3 pos = new Vector3(x, y, 0);
            GameObject newObject = Instantiate(fieldPref, pos, Quaternion.identity, transform) as GameObject;
            Fields.Add(newObject.GetComponent<YootField>());
        }
        for (int i = 0; i < YootFieldCount; i++)
        {
            Fields[i].Init(i);
        }
    }
}
