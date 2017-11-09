using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootBoard : MonoBehaviour {
    public GameObject fieldPref;
    public List<GameObject> fields;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        fields = YootFieldFactory.CreateYootFields(fieldPref, transform);
    }
}
