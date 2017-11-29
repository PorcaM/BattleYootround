using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;

public class ExitBattleDecorator : MonoBehaviour {
    public void ShowVictory(int winner)
    {
        string aliveTag;
        if (winner == 0)
            aliveTag = "AllyUnit";
        else
            aliveTag = "EnemyUnit";
        GameObject[] alives = GameObject.FindGameObjectsWithTag(aliveTag);
        foreach (GameObject obj in alives)
            obj.GetComponent<UnitInstance>().unitAnimation.SetAction(UnitAnimation.Actions.Victory);
        Vector3 offset = new Vector3(0, 1, 1);
        Camera.main.transform.position = alives[0].transform.position + offset;
        Camera.main.transform.LookAt(alives[0].transform.position);
        ToastManager.Show("Player " + winner + " Win!!");
    }
}
