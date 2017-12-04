using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitInstance))]
public class UnitStatBuff : MonoBehaviour {
    private UnitInstance unitInstance;

    public float attackSpeed;
    public float moveSpeed;
    public float maxHP;
    public float duration;
    
    public void Init(float attackSpeed, float moveSpeed, float maxHP, float duration)
    {
        unitInstance = GetComponent<UnitInstance>();
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.maxHP = maxHP;
        this.duration = duration;
    }

    public void Activate()
    {
        Apply();
        unitInstance.unitAnimation.Play(UnitAnimation.Actions.Damage, 1.0f);
        StartCoroutine(RecoverAfter(duration));
    }

    private void Apply()
    {
        unitInstance.attackSpeed += attackSpeed;
        unitInstance.movementSpeed += moveSpeed;
        unitInstance.MaxHp += maxHP;
    }

    private IEnumerator RecoverAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        Recover();
    }

    public void Recover()
    {
        unitInstance.attackSpeed -= attackSpeed;
        unitInstance.movementSpeed -= moveSpeed;
        unitInstance.MaxHp -= maxHP;
        Destroy(this);
    }
}
