using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour {
    public UnitHealthBar unitHealthBar;
    public string enemyTag;
    
    public Unit.ClassType unitClass;
    public int id;
    public float damage;
    public float armor;
    public float range;
    public float currentHP;
    public float maxHp;
    public float movementSpeed;
    public float attackSpeed;

    public const float attackCooltime = 1.0f;
    private float attackCooldown;

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

    public void Init(Unit unit)
    {
        unitClass = unit.UnitClass;
        id = unit.Id;
        damage = (float)unit.Damage;
        armor = (float)unit.Armor;
        range = (float)unit.Range;
        CurrentHP = maxHp = (float)unit.Hp;
        movementSpeed = (float)unit.MovementSpeed;
        attackSpeed = (float)unit.AttackSpeed;
        SetEnemyTag();
    }

    private void SetEnemyTag()
    {
        if (tag == "AllyUnit")
            enemyTag = "EnemyUnit";
        else if (tag == "EnemyUnit")
            enemyTag = "AllyUnit";
        else
            Debug.Log("Invalid unit tag");
    }

    public void UnderAttack(float damage)
    {
        CurrentHP -= damage;
        if (IsDead())
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return CurrentHP <= 0.0f;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        DoBattle();
        UpdateAttackCooltime();
    }

    private void DoBattle()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(enemyTag);
        if (IsEnemyExist(objs))
        {
            GameObject closestObject = FindClosestObject(objs);
            float distance = Vector3.Distance(closestObject.transform.position, transform.position);
            if (IsInAttackRange(distance))
                MoveTo(closestObject.transform);
            else
                Attack(closestObject.GetComponent<UnitInstance>());
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

    private bool IsInAttackRange(float distance)
    {
        return distance > range * 2;
    }
        
    private void MoveTo(Transform target)
    {
        RotateTo(target);
        MoveForward();
    }

    private void RotateTo(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;
        float step = movementSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

    }

    private void Attack(UnitInstance target)
    {
        if (IsAttackable())
        {
            target.UnderAttack(damage);
            attackCooldown = attackCooltime;
        }
    }

    private bool IsAttackable()
    {
        return AttackCooldown <= 0.0f;
    }

    private void UpdateAttackCooltime()
    {
        AttackCooldown -= Time.deltaTime * attackSpeed;
    }
}
