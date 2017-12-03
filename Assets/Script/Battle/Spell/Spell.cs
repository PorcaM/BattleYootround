using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell {
    public enum Type { Attack,Assist,Special};
    public Type type;
    private int id;
    private string spellName;
    private Range range;
    private float damage;
    private float duration;
    private float cooltime;
    public string target;
    public string description;


    #region Properties
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

    public string SpellName
    {
        get
        {
            return spellName;
        }

        set
        {
            spellName = value;
        }
    }
    #endregion

    public void Init()
    {
        type = Type.Attack;
        Id = 0;
        SpellName = "Basic";
        Range = new CircleRange(5.0f);
        Damage = 20.0f;
        Duration = 0.0f;
        Cooltime = 5.0f;
    }

    public void Copy(Spell spell)
    {
        Debug.Log(spell.SpellName);
        type = spell.type;
        id = spell.Id;
        spellName = spell.SpellName;
        range = spell.Range;
        damage = spell.Damage;
        duration = spell.Duration;
        cooltime = spell.Cooltime;
    }
    
    public new string ToString()
    {
        string info = "";
        info += "Name: " + spellName + "\n";
        info += description + "\n";
        info += "Type: " + type + "\n";
        info += "Amount: " + Damage + "\n";
        info += "Cooltime: " + cooltime + "\n";
        return info;
    }
}
