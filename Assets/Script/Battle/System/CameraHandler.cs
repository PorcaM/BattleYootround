using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform center;
    public float height = 6.0f;
    public Vector3 basicPosition;

    [SerializeField] private Vector3 backupPosition;
    [SerializeField] private Quaternion backupRotation;

    public void Init()
    {
        backupPosition = Camera.main.transform.position;
        backupRotation = Camera.main.transform.rotation;
        basicPosition = new Vector3(center.position.x, height, center.position.z - 1);
    }

    public void Recover()
    {
        Camera.main.transform.position = backupPosition;
        Camera.main.transform.rotation = backupRotation;
    }

    public void GoBattleField()
    {
        Camera.main.transform.position = basicPosition;
        Camera.main.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }
}
