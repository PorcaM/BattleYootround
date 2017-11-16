using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour {
    public UnitHealthBar unitHealthBar;
    
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
    public float attackCooldown;

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
    }

    private void DoBattle()
    {
        
        UnitInstance[] Enemies = FindEnemies();
        UnitInstance closestEnemy = FindClosestUnit(Enemies);
        float distance = CalcDistance(closestEnemy);
        if (distance > range)
            Move(closestEnemy);
        else
            Attack(closestEnemy);
    }

    private UnitInstance[] FindEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(GetEnemyTag());
        UnitInstance[] enemies = new UnitInstance[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].GetComponent<UnitInstance>();
        }
        return enemies;
    }
    
    private string GetEnemyTag()
    {
        if (tag == "AllyUnit")
        {
            return "EnemyUnit";
        }
        else if (tag == "EnemyUnit")
        {
            return "AllyUnit";
        }
        else
        {
            Debug.Log("Invalid unit tag");
            return "";
        }
    }

    private UnitInstance FindClosestUnit(UnitInstance[] units)
    {
        if (units.Length == 0)
            return null;
        UnitInstance closestUnit = units[0];
        float closestDistance = CalcDistance(units[0]);
        foreach(UnitInstance unit in units)
        {
            float distance = CalcDistance(unit);
            if (closestDistance > distance)
                closestUnit = unit;
        }
        return closestUnit;
    }

    private float CalcDistance(UnitInstance targetUnit)
    {
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = targetUnit.transform.position;
        return Vector3.Distance(targetPosition, myPosition);
    }
    
    private void Move(UnitInstance targetUnit)
    {
        Vector3 direction = targetUnit.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * movementSpeed);
    }

    private void Attack(UnitInstance targetUnit)
    {
        if (attackCooldown > 0.0f)
        {
            attackCooldown -= Time.deltaTime * attackSpeed;
        }
        else
        {
            targetUnit.UnderAttack(damage);
            attackCooldown = attackCooltime;
        }
    }
}
