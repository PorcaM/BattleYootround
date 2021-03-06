﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellArea : MonoBehaviour {
    public SpellActivator spellActivator;
    public float radius;

    void OnMouseDown()
    {
        FinishAnimation();
    }

    private void FinishAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = Quaternion.identity;
    }

    void OnMouseDrag()
    {
        UpdatePosition();
    }

    void OnMouseUp()
    {
        spellActivator.ActivateSpell();
    }

    private void TestRadius()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= radius)
            {
                enemy.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
    private void UpdatePosition()
    {
        float distance = Camera.main.transform.position.y;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        transform.position = rayPoint;
        transform.position = new Vector3(transform.position.x, .1f, transform.position.z);
    }
}