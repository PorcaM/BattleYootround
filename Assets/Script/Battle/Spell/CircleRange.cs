using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRange : Range
{
    private Vector2 point;
    public float radius;

    public float Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public CircleRange(float radius)
    {
        Init(radius);
    }

    public void Init(float radius)
    {
        Radius = radius;
    }

    public override List<UnitInstance> SelectTarget(Vector3 pos)
    {
        List<UnitInstance> targets = new List<UnitInstance>();
        GameObject[] allies = GameObject.FindGameObjectsWithTag("AllyUnit");
        foreach (GameObject ally in allies)
        {
            float distance = Vector3.Distance(ally.transform.position, pos);
            if (distance <= radius)
                targets.Add(ally.GetComponent<UnitInstance>());
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, pos);
            if (distance <= radius)
                targets.Add(enemy.GetComponent<UnitInstance>());
        }
        return targets;
    }
}
