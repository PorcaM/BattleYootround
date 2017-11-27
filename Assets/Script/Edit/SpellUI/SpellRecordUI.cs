using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRecordUI : MonoBehaviour {
    public Transform spellParent;
    public SpellUIFactory factory;
    public ScrollRect scrollRect;

    public void Init()
    {
        foreach (Spell spell in SpellRecord.spells)
        {
            if (spell.SpellName != "Basic")
                factory.Create(spell, spellParent);
        }
    }

    public void Next()
    {
        scrollRect.horizontalNormalizedPosition += .3f;
    }

    public void Back()
    {
        scrollRect.horizontalNormalizedPosition -= .3f;
    }
}
