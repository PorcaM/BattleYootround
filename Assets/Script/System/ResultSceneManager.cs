using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour {
    public Text text;

	void Start () {
        int winner = GameObject.Find("YootGameResult").GetComponent<YootGameResult>().winner;
        SoundManager.Instance().PlayMusic(winner);
        text.text = "Winner is Player " + winner;
    }
}
