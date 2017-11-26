using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowProcessor : MonoBehaviour {
    public YootPlayer owner;
    public TurnProcessor turnManager;
    public enum ProcessState { Start, Wait };
    public ProcessState currentState;

    public ThrowModule throwModule;
    private ThrowModule createdModule;

    private DecoThrowResult deco;

    void Start()
    {
        currentState = ProcessState.Wait;
        deco = GameObject.Find("Decos").GetComponent<DecoThrowResult>();
    }

    public void StartThrow()
    {
        currentState = ProcessState.Start;
        createdModule = Instantiate(throwModule, transform) as ThrowModule;
        createdModule.Init(this);
    }

    public void RecvThrowResult(YootGame.YootCount result)
    {
        if (currentState == ProcessState.Start)
            HandleThrowResult(result);
    }

    private void HandleThrowResult(YootGame.YootCount result)
    {
        deco.Show(result, 1.5f);
        createdModule.End();
        turnManager.RecvThrowResult(result);
        currentState = ProcessState.Wait;
    }

    private static YootGame.YootCount SimpleRandom()
    {
        int count = Enum.GetValues(typeof(YootGame.YootCount)).Length;
        return (YootGame.YootCount)UnityEngine.Random.Range(0, count) - 1;
    }
}
