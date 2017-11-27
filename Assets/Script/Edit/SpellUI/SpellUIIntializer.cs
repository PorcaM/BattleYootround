using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUIIntializer : MonoBehaviour {

    public SpellbookUI spellbookUI;
    public SpellRecordUI spellRecordUI;

    public Spellbook spellbook;

    public void Init(Spellbook spellbook)
    {
        this.spellbook = spellbook;
        spellbookUI.Init(spellbook);
        spellRecordUI.Init();
    }
}
