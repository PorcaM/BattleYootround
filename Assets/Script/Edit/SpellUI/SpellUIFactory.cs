using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUIFactory : MonoBehaviour {
    public SpellUI original;

    public SpellUI Create(Spell spell, Transform parent)
    {
        SpellUI spellUI = Instantiate(original, parent);
        spellUI.Init(spell);
        spellUI.name = "SpellUI:" + spell.SpellName;
        return spellUI;
    }
}
