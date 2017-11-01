using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Type { Warrior, Archer, HorseSoldier, Spearman, Thief, Wizard, Paladin };
    public Type type;
    public double damage;

    public void Init()
    {
        type = Type.Warrior;
        damage = 0.0;
    }
}
