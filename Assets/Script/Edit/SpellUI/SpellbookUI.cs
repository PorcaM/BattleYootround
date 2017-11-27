using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookUI : MonoBehaviour {
    public Transform spellParent;
    public SpellUIFactory factory;
    public Spellbook spellbook;
    public List<SpellUI> uiList;

    void Start()
    {
        uiList = new List<SpellUI>();
    }

    public void Init(Spellbook spellbook)
    {
        this.spellbook = spellbook;
        uiList.Clear();
        foreach (Spell spell in spellbook.spells)
            CreateSpellUI(spell);
    }

    private void CreateSpellUI(Spell spell)
    {
        SpellUI spellUI = factory.Create(spell, spellParent);
        spellUI.gameObject.AddComponent<EditableSpell>().Init(this, false);
        uiList.Add(spellUI);
    }

    public void Add(Spell spell)
    {
        spellbook.spells.Add(spell);
        CreateSpellUI(spell);
    }

    public void Remove(SpellUI spellUI)
    {
        spellbook.spells.Remove(spellUI.spell);
        uiList.Remove(spellUI);
        Destroy(spellUI.gameObject);
    }
}
