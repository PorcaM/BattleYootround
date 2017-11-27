using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookUI : MonoBehaviour {
    public Transform spellParent;
    public SpellUIFactory factory;
    public ScrollRect scrollRect;
    public Spellbook spellbook;

    public void Init(Spellbook spellbook)
    {
        this.spellbook = spellbook;
        foreach (Spell spell in spellbook.spells)
        {
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

    public void Add(Spell spell)
    {
        spellbook.spells.Add(spell);
        factory.Create(spell, spellParent);
    }

    public void Remove(Spell spell)
    {
        spellbook.spells.Remove(spell);
        foreach(SpellUI spellUI in spellParent.GetComponentsInChildren<SpellUI>())
        {
            if(spellUI.spell == spell)
            {
                Destroy(spellUI.gameObject);
                break;
            }
        }
    }
}
