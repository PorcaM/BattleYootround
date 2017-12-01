using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject model;

    public enum Actions
    {
        Idle = 1, Alert, Victory, Damage = 5, Die = 7, Attack = 11, Shoot = 14, Move = 15,
    }
    [SerializeField] private Actions action;
    [SerializeField] private float speed;

    public void Init(GameObject model)
    {
        this.model = model;
        animator = model.GetComponent<Animator>();
    }

    public void Play(Actions action, float speed)
    {
        this.action = action;
        this.speed = speed;
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetInteger("animation", (int)action);
        animator.speed = speed;
    }

    public void SetAction(Actions action)
    {
        animator.SetInteger("animation", (int)action);
    }

    public void SetSpeed(float speed)
    {
        animator.speed = speed;
    }
}
