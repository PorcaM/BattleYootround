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
                if (spell.Damage > 0)
                    target.UnderAttack(spell.Damage);
                else
                    target.Recovery(-spell.Damage);
            }
        }
    }
}
