using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public ThrowModule throwManager;
    public GameObject[] targets = new GameObject[4];
    public bool isThrowed;
    public bool isAllStop;
    public bool isEnoughHeight = false;

    public void StartDetect()
    {
        isThrowed = true;
    }

    private void Update()
    {
        if (isThrowed)
            Detect();
    }

    private void Detect()
    {
        DetectHeight();
        DetectNak();
        DetectNormalResult();
    }

    private void DetectHeight()
    {
        if (!isEnoughHeight)
        {
            foreach (GameObject target in targets)
            {
                if (target.GetComponent<DetectMinHeight>().enough)
                {
                    isEnoughHeight = true;
                    break;
                }
            }
        }
    }

    private void DetectNak()
    {
        foreach (GameObject target in targets)
            if (target.GetComponent<DetectFallen>().isFallen)
            {
                SendResult(YootGame.YootCount.Nak);
                break;
            }
    }

    private void SendResult(YootGame.YootCount result)
    {
        if (isEnoughHeight)
        {
            throwManager.RecvThrowResult(result);
            isThrowed = false;
        }
        else
            throwManager.throwProcessor.ThrowAgain();
    }

    private void DetectNormalResult()
    {
        UpdateIsAllStop();
        if (isAllStop)
            ReadResult();
    }

    private void UpdateIsAllStop()
    {
        isAllStop = true;
        foreach (GameObject target in targets)
            if (target.GetComponent<DetectMove>().IsMoving)
            {
                isAllStop = false;
                break;
            }
    }

    private void ReadResult()
    {
        YootGame.YootCount result = GetResult();
        SendResult(result);
    }

    private YootGame.YootCount GetResult()
    {
        YootGame.YootCount yootCount;
        int numDownside = GetNumDownside();
        if (numDownside == 0)
        {
            yootCount = YootGame.YootCount.Mo;
        }
        else
        {
            yootCount = (YootGame.YootCount)numDownside;
            if (numDownside == 1 &&
                targets[0].GetComponent<DetectDownside>().isDownside)
            {
                yootCount = YootGame.YootCount.BackDo;
            }
        }
        return yootCount;
    }

    private int GetNumDownside()
    {
        int count = 0;
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<DetectDownside>().isDownside)
                count++;
        }
        return count;
    }
}
