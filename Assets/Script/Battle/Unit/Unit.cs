using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public enum ClassType { Basic, Knight, Archer, Dark, Spearman, Rogue, Wizard, Paladin };
    private ClassType unitClass;
    private int id;
    private double damage;
    private double armor;
    private double range;
    private double hp;
    private double movementSpeed;
    private double attackSpeed;
    public int position;
    

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

    public double Damage
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

    public double Range
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

    public double Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    public double MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }

    public double AttackSpeed
    {
        get
        {
            return attackSpeed;
        }

        set
        {
            attackSpeed = value;
        }
    }

    public double Armor
    {
        get
        {
            return armor;
        }

        set
        {
            armor = value;
        }
    }

    public ClassType UnitClass
    {
        get
        {
            return unitClass;
        }

        set
        {
            unitClass = value;
        }
    }

    public void Init()
    {
        UnitClass = ClassType.Basic;
        Damage = 10.0;
        Armor = 0.0;
        Range = 10.0;
        Hp = 50.0;
        MovementSpeed = 3.0;
        AttackSpeed = 1.0;
    }

    public new string ToString()
    {
        string info = "";
        info += "Class: " + UnitClass.ToString() + "\n";
        info += "Damage: " + Damage + "\n";
        info += "Armor: " + Armor + "\n";
        info += "Range: " + Range + "\n";
        info += "Hp: " + Hp + "\n";
        info += "MovementSpeed: " + MovementSpeed + "\n";
        info += "AttackSpeed: " + AttackSpeed + "\n";
        return info;
    }
}
