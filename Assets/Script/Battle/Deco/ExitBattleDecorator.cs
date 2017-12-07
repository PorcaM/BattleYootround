using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class ExitBattleDecorator : MonoBehaviour {
    public Text BattleResultText;

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
        Vector3 closeupPos = alives[0].transform.position + offset;
        Vector3 backupPos = Camera.main.transform.position;
        Camera.main.transform.position = closeupPos;
        Camera.main.transform.LookAt(alives[0].transform.position);
        Camera.main.gameObject.AddComponent<HorseAnimator>().Init(backupPos, closeupPos, 1.0f);
        BattleResultText.enabled = true;
        BattleResultText.text = "Your Win!\n^^";
        if (winner == 1)
            BattleResultText.text = "Your loss\nㅠㅠ";
        ToastManager.Show("Player " + winner + " Win!!");
    }

    private void AnimateAlives(GameObject[] alives)
    {
        foreach (GameObject obj in alives)
        {
            UnitInstance unitInstance = obj.GetComponent<UnitInstance>();
            unitInstance.transform.rotation = Quaternion.identity;
            unitInstance.unitAnimation.Play(UnitAnimation.Actions.Victory);
        }
    }
}
