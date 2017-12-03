using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManifestator : MonoBehaviour {
    public static void AllySpell(Vector3 pos, int spellId)
    {
        Spell spell = SpellRecord.spells[spellId];
        List<UnitInstance> targets = spell.Range.SelectTarget(pos);
        string targetTag = "EnemyUnit";
        if (spell.target == "AllyUnit")
            targetTag = "AllyUnit";
        Apply(targets, targetTag, spell);
    }

    public static void EnemySpell(Vector3 pos, int spellId)
    {
        pos = new Vector3(pos.x, pos.y, -pos.z);
        Spell spell = SpellRecord.spells[spellId];
        List<UnitInstance> targets = spell.Range.SelectTarget(pos);
        string targetTag = "AllyUnit";
        if (spell.target == "AllyUnit")
            targetTag = "EnemyUnit";
        Apply(targets, targetTag, spell);
    }

    private static void Apply(List<UnitInstance> targets, string targetTag, Spell spell)
    {
        foreach (UnitInstance target in targets)
        {
            if (target.tag == targetTag)
            {
                if (spell.type == Spell.Type.Attack)
                    target.UnderAttack(spell.Damage);
                else
                {
                    if (spell.SpellName == "Heal")
                        target.Recovery(spell.Damage);
                    else if (spell.SpellName == "Cleanse")
                    {
                        UnitStatBuff buff = target.GetComponent<UnitStatBuff>();
                        while (buff != null)
                        {
                            buff.Recover();
                            buff = target.GetComponent<UnitStatBuff>();
                        }
                    }
                    else
                        HandleAssistType(target, spell);
                }
            }
        }
    }

    private static void HandleAssistType(UnitInstance target, Spell spell)
    {
        UnitStatBuff buff = target.gameObject.AddComponent<UnitStatBuff>();
        float attackSpeed = 0.0f;
        float moveSpeed = 0.0f;
        float maxHP = 0.0f;
        float duration = spell.Duration;
        switch (spell.SpellName)
        {
            case "SteamPack":
                attackSpeed = 0.5f;
                moveSpeed = 0.5f;
                break;
            case "Grasping":
                moveSpeed = -target.movementSpeed;
                break;
            case "Stun":
                attackSpeed = -target.attackSpeed;
                moveSpeed = -target.movementSpeed;
                break;
            case "Infection":
                maxHP -= 30.0f;
                break;
        }
        buff.Init(attackSpeed, moveSpeed, maxHP, duration);
        buff.Activate();
    }
}
