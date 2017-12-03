using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform center;
    public float height = 6.0f;
    public Vector3 basicPosition;
    public bool isCloseup;
    public float closeupHeight = 2.0f;
    public GameObject UI;
    public DoubleClickListener doubleClickListener;

    private float doubleClickTimeLimit = 0.25f;
    [SerializeField] private Vector3 backupPosition;
    [SerializeField] private Quaternion backupRotation;

    public void Init()
    {
        backupPosition = Camera.main.transform.position;
        backupRotation = Camera.main.transform.rotation;
        basicPosition = new Vector3(center.position.x, height, center.position.z - 1);
        doubleClickListener.Init(DoubleClick);
    }

    public void Cleanup()
    {
        Recover();
        doubleClickListener.Cleanup();
    }

    private void Recover()
    {
        if (isCloseup)
            BackCloseup();
        Camera.main.transform.position = backupPosition;
        Camera.main.transform.rotation = backupRotation;
    }

    public void Setup()
    {
        GoBattleField();
        doubleClickListener.Setup();
        isCloseup = false;
    }

    private void GoBattleField()
    {
        Camera.main.transform.position = basicPosition;
        Camera.main.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }

    public void GoCloseup()
    {
        UI.SetActive(false);
        float distance = Camera.main.transform.position.y;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        Vector3 closeupPosition = new Vector3(rayPoint.x, closeupHeight, rayPoint.z);
        //Camera.main.transform.position = closeupPosition;
        Camera.main.gameObject.AddComponent<HorseAnimator>().Init(Camera.main.transform.position, closeupPosition, 1.0f);
        Camera.main.gameObject.AddComponent<CameraDrag>().isEnable = true;
        FloatingTextController.isWorking = false;
        isCloseup = true;
    }

    public void BackCloseup()
    {
        UI.SetActive(true);
        //Camera.main.transform.position = basicPosition;
        Camera.main.gameObject.AddComponent<HorseAnimator>().Init(Camera.main.transform.position, basicPosition, 1.0f);
        CameraDrag cameraDrag = Camera.main.gameObject.GetComponent<CameraDrag>();
        Debug.Log(cameraDrag);
        Destroy(cameraDrag);
        FloatingTextController.isWorking = true;
        isCloseup = false;
    }

    private void DoubleClick()
    {
        if (isCloseup)
            BackCloseup();
        else
            GoCloseup();
    }
}
