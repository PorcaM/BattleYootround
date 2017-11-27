using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRecordUI : MonoBehaviour {
    public Transform spellParent;
    public SpellUIFactory factory;
    public SpellbookUI spellbookUI;

    public void Init()
    {
        foreach (Spell spell in SpellRecord.spells)
        {
            if (spell.SpellName != "Basic")
            {
                SpellUI spellUI = factory.Create(spell, spellParent);
                spellUI.gameObject.AddComponent<EditableSpell>().Init(spellbookUI, true);
            }
        }
    }
}
