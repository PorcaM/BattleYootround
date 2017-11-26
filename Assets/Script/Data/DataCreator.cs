using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreator : MonoBehaviour {
    public void Create()
    {
        GameObject data = new GameObject();
        data.name = "Data";
        DontDestroyOnLoad(data);
    }
}
