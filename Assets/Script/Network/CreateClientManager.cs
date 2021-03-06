﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateClientManager : MonoBehaviour {

    public GameObject clientManager;
	// Use this for initialization
	void Start () {
        GameObject check = GameObject.Find("ClientManager");
        Debug.Log(check);
        if(check == null)
        {
            Debug.Log("Create client Manager!");
            clientManager = Instantiate(clientManager);
            clientManager.name = "ClientManager";
            //DontDestroyOnLoad(clientManager);

        }
        else
        {
            //clientManager.GetComponent<BYClient>().Start();
        }

        UnityEngine.UI.Button match = GameObject.Find("Match").GetComponent<UnityEngine.UI.Button>();
        match.onClick.AddListener(clientManager.GetComponent<BYClient>().ConnectToServer);

        //UnityEngine.UI.Button cancel = GameObject.Find("Cancel").GetComponent<UnityEngine.UI.Button>();
        //cancel.onClick.AddListener(clientManager.GetComponent<BYClient>().Cancel);

        YootGame.isNetwork = true;
    }
}
