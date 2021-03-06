﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitInstance : NetworkBehaviour {
    public UnitHealthBar unitHealthBar;
    public UnitAnimation unitAnimation;
    public CharacterController controller;
    public string enemyTag;
    public enum State { Alive, Dead, Ready }
    public State currentState;

    public Unit.ClassType unitClass;
    public int id;
    public float damage;
    public float armor;
    public float range;
    [SyncVar]
    public float currentHP;
    private float maxHp;
    public float movementSpeed;
    public float attackSpeed;

    public const float attackCooltime = 1.0f;
    [SerializeField] private float attackCooldown;
    const float unitSize = 0.25f;    

    public float CurrentHP
    {
        get
        {
            return currentHP;
        }

        set
        {
            currentHP = value;
            if (currentHP < 0.0f)
                currentHP = 0.0f;
            if (currentHP > maxHp)
                currentHP = maxHp;
            unitHealthBar.CurrentHealth = currentHP;
        }
    }

    public float AttackCooldown
    {
        get
        {
            return attackCooldown;
        }

        set
        {
            attackCooldown = value;
            if (attackCooldown < 0.0f)
                attackCooldown = 0.0f;
        }
    }

    public float MaxHp
    {
        get
        {
            return maxHp;
        }

        set
        {
            maxHp = value;
            unitHealthBar.MaxHealth = maxHp;
        }
    }

    public void Init(Unit unit)
    {
        unitClass = unit.UnitClass;
        id = unit.Id;
        damage = (float)unit.Damage;
        armor = (float)unit.Armor;
        range = (float)unit.Range;
        CurrentHP = MaxHp = (float)unit.Hp;
        movementSpeed = (float)unit.MovementSpeed;
        attackSpeed = (float)unit.AttackSpeed;
        SetupEnemyTag();
        currentState = State.Ready;
        controller = GetComponent<CharacterController>();
    }

    private void SetupEnemyTag()
    {
        if (tag == "AllyUnit")
            enemyTag = "EnemyUnit";
        else if (tag == "EnemyUnit")
            enemyTag = "AllyUnit";
        else
            Debug.Log("Invalid unit tag");
    }

    public void Recovery(float amount)
    {
        CurrentHP += amount;
        if (FloatingTextController.isWorking)
            FloatingTextController.CreateFloatingText(amount.ToString(), transform, "#4CAF50FF");
    }

    public void UnderAttack(float damage)
    {
        damage -= armor;
        if (FloatingTextController.isWorking)
            FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        CurrentHP -= damage;
        if (IsDead())
            Die();
    }

    public bool IsDead()
    {
        return CurrentHP <= 0.0f;
    }

    public void Die()
    {
        currentState = State.Dead;
        tag = "DeadUnit";
        unitAnimation.Play(UnitAnimation.Actions.Die);
        Destroy(gameObject, 1.0f);
        CombatManager.Instance().CheckBattleOver();
    }

    void Update()
    {
        if (currentState == State.Alive)
        {
            DoBattle();
            UpdateAttackCooltime();
        }
    }

    private void DoBattle()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(enemyTag);
        if (IsEnemyExist(objs))
        {
            GameObject closest = FindClosestObject(objs);
            if (IsInAttackRange(closest.transform.position))
                Attack(closest.GetComponent<UnitInstance>());
            else
                MoveTo(closest.transform);
        }
    }

    private bool IsEnemyExist(GameObject[] objs)
    {
        return objs.Length > 0;
    }

    private GameObject FindClosestObject(GameObject[] objs)
    {
        float minDistance = Vector3.Distance(objs[0].transform.position, transform.position);
        GameObject closestObject = objs[0];
        foreach(GameObject obj in objs)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance < minDistance)
            {
                closestObject = obj;
                minDistance = distance;
            }
        }
        return closestObject;
    }

    private bool IsInAttackRange(Vector3 point)
    {
        float distance = Vector3.Distance(point, transform.position);
        return distance <= range * unitSize;
    }
        
    private void MoveTo(Transform target)
    {
        // if (controller.isGrounded)
        {
            Vector3 moveDir = target.position - transform.position;
            moveDir.Normalize();
            controller.Move(moveDir * Time.deltaTime * movementSpeed / 2);
            unitAnimation.Play(UnitAnimation.Actions.Move, movementSpeed);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void Attack(UnitInstance target)
    {
        if (IsAttackable())
        {
            target.UnderAttack(damage);
            attackCooldown = attackCooltime;
            UnitAnimation.Actions action = GetAttackAction();
            unitAnimation.Play(action, attackSpeed);
        }
    }

    private bool IsAttackable()
    {
        return AttackCooldown <= 0.0f;
    }

    private UnitAnimation.Actions GetAttackAction()
    {
        UnitAnimation.Actions action;
        switch (unitClass)
        {
            case Unit.ClassType.Archer:
                action = UnitAnimation.Actions.Shoot;
                break;
            case Unit.ClassType.Knight:
                action = UnitAnimation.Actions.GuardAttack;
                break;
            case Unit.ClassType.Dark:
            case Unit.ClassType.Paladin:
                action = UnitAnimation.Actions.JumpAttack;
                break;
            default:
                action = UnitAnimation.Actions.Attack;
                break;
        }
        return action;
    }

    private void UpdateAttackCooltime()
    {
        AttackCooldown -= Time.deltaTime * attackSpeed;
    }
}
