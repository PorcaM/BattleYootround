using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance : MonoBehaviour {
    public float cooldown;
    public float ratio;

    public Spell.Type spellType;
    public string spellName;
    public int id;
    public Range range;
    public float damage;
    public float duration;
    public float cooltime;
    public float radius;

    public SpellManager spellManager;

    public float Cooldown
    {
        get
        {
            return cooldown;
        }

        set
        {
            cooldown = value;
            if (cooldown < 0.0f)
                cooldown = 0.0f;
        }
    }

    public float Ratio
    {
        get
        {
            return ratio;
        }

        set
        {
            ratio = value;
        }
    }

    public void Init(Spell spell)
    {
        spellType = spell.type;
        spellName = spell.SpellName;
        id = spell.Id;
        range = spell.Range;
        damage = spell.Damage;
        duration = spell.Duration;
        cooltime = spell.Cooltime;
        radius = ((CircleRange)range).radius;
    }

    void Update()
    {
        Cooldown -= Time.deltaTime;
        Ratio = Cooldown / cooltime;
    }

    public void Selected()
    {
        if (Cooldown <= 0.0f)
            spellManager.Select(this);
    }

    public void Activated()
    {
        Cooldown = cooltime;
    }
}
