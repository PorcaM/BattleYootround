using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour {
    private Unit myUnit;
    private UnitInstance[] enemies;
    private string enemyTag;
    
    public Unit MyUnit
    {
        get
        {
            return myUnit;
        }

        set
        {
            myUnit = value;
        }
    }

    public UnitInstance[] Enemies
    {
        get
        {
            return enemies;
        }

        set
        {
            enemies = value;
        }
    }

    void Start()
    {
        MyUnit = new Unit();
        MyUnit.Init();
    }

    public void UnderAttack(double damage)
    {
        MyUnit.Hp -= damage;
        if (IsDead())
        {
            Die();
        }
    }

    private bool IsDead()
    {
        return MyUnit.Hp <= 0.0;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        DoBattle();
    }

    public void DoBattle()
    {
        Enemies = FindEnemies();
        UnitInstance closestEnemy = FindClosestUnit(Enemies);
        double distance = CalcDistance(closestEnemy);
        if (distance > MyUnit.Range)
            Move(closestEnemy);
        else
            Attack(closestEnemy);
    }

    private UnitInstance[] FindEnemies()
    {
        if (enemyTag == null)
            SetEnemyTag();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemyTag);
        UnitInstance[] enemies = new UnitInstance[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].GetComponent<UnitInstance>();
        }
        return enemies;
    }

    private void SetEnemyTag()
    {
        enemyTag = "AllyUnit";
        if (this.tag == "AllyUnit")
            enemyTag = "EnemyUnit";
    }

    private UnitInstance FindClosestUnit(UnitInstance[] units)
    {
        UnitInstance closestUnit = units[0];
        double closestDistance = CalcDistance(units[0]);
        foreach(UnitInstance unit in units)
        {
            double distance = CalcDistance(unit);
            if (closestDistance > distance)
                closestUnit = unit;
        }
        return closestUnit;
    }

    private double CalcDistance(UnitInstance targetUnit)
    {
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = targetUnit.transform.position;
        return Vector3.Distance(targetPosition, myPosition);
    }
    
    private void Move(UnitInstance targetUnit)
    {
        Vector3 direction = targetUnit.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * (float)MyUnit.MovementSpeed);
    }

    private void Attack(UnitInstance targetUnit)
    {
        targetUnit.UnderAttack(MyUnit.Damage);
    }
}
