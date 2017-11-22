using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour {
    public UnitHealthBar unitHealthBar;
    public UnitAnimation unitAnimation;
    public CharacterController characterController;
    public string enemyTag;
    public enum State { Alive, Dead }
    public State currentState;
    
    public Unit.ClassType unitClass;
    public int id;
    public float damage;
    public float armor;
    public float range;
    public float currentHP;
    private float maxHp;
    public float movementSpeed;
    public float attackSpeed;

    public const float attackCooltime = 3.0f;
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
        SetEnemyTag();
        currentState = State.Alive;
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
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
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
        currentState = State.Dead;
        tag = "DeadUnit";
        unitAnimation.SetAction(UnitAnimation.Actions.Die);
        Destroy(gameObject, 1.0f);
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
        return distance <= (range - 1) + .3f;
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
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed / 2);
        unitAnimation.SetAction(UnitAnimation.Actions.Move);
    }

    private void Attack(UnitInstance target)
    {
        if (IsAttackable())
        {
            target.UnderAttack(damage);
            attackCooldown = attackCooltime;
            unitAnimation.SetAction(UnitAnimation.Actions.Attack);
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
