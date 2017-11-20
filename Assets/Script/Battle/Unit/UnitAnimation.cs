using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour {
    public Animator animator;

    private string constText = "animation";
    public enum Actions
    {
        Idle = 1, Alert, Victory, Damage = 5, Die = 7, Attack = 11, Shoot = 14, Move = 15,
    }

    public void SetAction(Actions action)
    {
        if (!animator)
            animator = GetComponent<Animator>();
        animator.SetInteger(constText, (int)action);
    }
}
