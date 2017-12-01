using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform center;
    public float height = 6.0f;
    public Vector3 basicPosition;
    public bool isCloseup;
    public float closeupHeight = 3.0f;
    public GameObject UI;

    private float doubleClickTimeLimit = 0.25f;
    [SerializeField] private Vector3 backupPosition;
    [SerializeField] private Quaternion backupRotation;

    public void Init()
    {
        backupPosition = Camera.main.transform.position;
        backupRotation = Camera.main.transform.rotation;
        basicPosition = new Vector3(center.position.x, height, center.position.z - 1);
    }

    public void Cleanup()
    {
        Recover();
        StopCoroutine(InputListener());
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
        StartCoroutine(InputListener());
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
        Camera.main.transform.position = closeupPosition;
        Camera.main.gameObject.AddComponent<CameraDrag>().isEnable = true;
        isCloseup = true;
    }

    public void BackCloseup()
    {
        UI.SetActive(true);
        Camera.main.transform.position = basicPosition;
        CameraDrag cameraDrag = Camera.main.gameObject.GetComponent<CameraDrag>();
        Destroy(cameraDrag);
        isCloseup = false;
    }

    // Update is called once per frame
    private IEnumerator InputListener()
    {
        while (enabled)
        { //Run as long as this is activ

            if (Input.GetMouseButtonDown(0))
                yield return ClickEvent();

            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //pause a frame so you don't pick up the same mouse down event.
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < doubleClickTimeLimit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoubleClick();
                yield break;
            }
            count += Time.deltaTime;// increment counter by change in time between frames
            yield return null; // wait for the next frame
        }
        SingleClick();
    }

    private void SingleClick()
    {
        //
    }

    private void DoubleClick()
    {
        if (isCloseup)
            BackCloseup();
        else
            GoCloseup();
    }
}
