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
        AnimateAlives(alives);
        Vector3 offset = new Vector3(0, 1, 1);
        Camera.main.transform.position = alives[0].transform.position + offset;
        Camera.main.transform.LookAt(alives[0].transform.position);
        ToastManager.Show("Player " + winner + " Win!!");
    }

    private void AnimateAlives(GameObject[] alives)
    {
        foreach (GameObject obj in alives)
        {
            UnitInstance unitInstance = obj.GetComponent<UnitInstance>();
            unitInstance.transform.rotation = Quaternion.identity;
            unitInstance.unitAnimation.SetSpeed(1.0f);
            unitInstance.unitAnimation.SetAction(UnitAnimation.Actions.Victory);
        }
    }
}
