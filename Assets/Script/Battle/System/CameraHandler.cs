using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform center;
    public float height = 7.5f;

    private Vector3 backupPosition;
    private Quaternion backupRotation;

    public void Backup()
    {
        backupPosition = Camera.main.transform.position;
        backupRotation = Camera.main.transform.rotation;
    }

    public void Recover()
    {
        Camera.main.transform.position = backupPosition;
        Camera.main.transform.rotation = backupRotation;
    }

    public void GoBattleField()
    {
        Camera.main.transform.position = new Vector3(center.position.x, height, center.position.z);
        Camera.main.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }
}
