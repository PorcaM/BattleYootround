using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreator : MonoBehaviour {
    public void Create()
    {
        GameObject data = Instantiate(new GameObject());
        data.name = "Data";
        DontDestroyOnLoad(data);
    }
}
