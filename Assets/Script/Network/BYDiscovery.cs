using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BYDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        // 서버로부터 브로드캐스트 메시지를 받을 시 실행
        base.OnReceivedBroadcast(fromAddress, data);
        Debug.Log("Received BroadCast from: " + fromAddress);

        StartCoroutine("TweenAnim");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
