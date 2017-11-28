using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowModule : MonoBehaviour
{
    public enum State { WaitTouch, WaitThrow, Throwed, Result }
    public State state;
    public ForceGenerator forceGenerator;
    public ResultManager resultManager;
    public AccelManager accelManager;
    public ThrowProcessor throwProcessor;
    public GameObject[] targets = new GameObject[4];

    private float maxHeight;
    private Vector3 backupPos;

    public void Init(ThrowProcessor throwProcessor)
    {
        this.throwProcessor = throwProcessor;
        state = State.WaitTouch;
        backupPos = Camera.main.transform.position;
        maxHeight = 0.0f;
    }

    public void RecvThrowResult(YootGame.YootCount count)
    {
        throwProcessor.RecvThrowResult(count);
        state = State.Result;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitTouch:
                HandleWaitTouchState();
                break;
            case State.WaitThrow:
                HandleWaitThrowState();
                break;
            case State.Throwed:
                ControlCamera();
                break;
            default:
                break;
        }
    }

    private void HandleWaitTouchState()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            state = State.WaitThrow;
        }
    }

    private void HandleWaitThrowState()
    {
        if (IsThrowOccur())
        {
            forceGenerator.ForceTargets();
            state = State.Throwed;
            StartCoroutine(StartDetectAfterSeconds(0.5f));
        }
    }

    private IEnumerator StartDetectAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        resultManager.StartDetect();
    }

    private bool IsThrowOccur()
    {
        bool computer = IsComputer();
        bool mobile = IsMobile();
        if (mobile)
        {
            const float powerDrag = 3.0f;
            forceGenerator.userPower = accelManager.MaxMagnitude() / powerDrag;
        }
        return computer || mobile;
    }

    private bool IsComputer()
    {
        return Input.GetKeyDown("space");
    }

    private bool IsMobile()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).phase == TouchPhase.Ended;
        else
            return false;
    }

    private void ControlCamera()
    {
        const float viewDistance = 3.0f;
        float properHeight = FindMaxHeight() + viewDistance;
        if (properHeight > Camera.main.transform.position.y)
        {
            float amount = properHeight - Camera.main.transform.position.y;
            Camera.main.transform.Translate(new Vector3(0, amount, 0), Space.World);
        }
    }

    private float FindMaxHeight()
    {
        float maxHeight = targets[0].transform.position.y;
        foreach(GameObject target in targets)
        {
            if (maxHeight < target.transform.position.y)
                maxHeight = target.transform.position.y;
        }
        return maxHeight;
    }

    public void End()
    {
        Camera.main.transform.position = backupPos;
        Destroy(gameObject);
    }
}
