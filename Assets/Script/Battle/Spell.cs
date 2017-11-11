using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    public enum Type { Attack,Assist,Special};
    public Type type;
    private int id;
    private string name;
    private Range range;
    private float damage;
    private float duration;
    private float cooltime;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Range Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public float Cooltime
    {
        get
        {
            return cooltime;
        }

        set
        {
            cooltime = value;
        }
    }

    public void Init()
    {
        type = Type.Attack;
        Id = 0;
        Name = "Basic";
        Range = new CircleRange(5.0f);
        Damage = 20.0f;
        Duration = 0.0f;
        Cooltime = 5.0f;
    }
}
